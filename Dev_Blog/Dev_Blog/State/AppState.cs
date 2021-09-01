using Dev_Blog.Data;
using Dev_Blog.Interfaces;
using Dev_Blog.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Dev_Blog.Pages.Posts;

namespace Dev_Blog.State
{
    public class AppState
    {
        public event Action<ComponentBase> StateChanged;

        public List<PostModel> Posts { get; private set; }

        public CommentVM Comment { get; private set; }

        private readonly ICommentRepository _comments;
        private readonly IPostRepository _posts;
        private readonly IVoteRepository _votes;
        private readonly IEmailRepository _email;
        private readonly IImageRepository _image;

        public AppState(IImageRepository image, IEmailRepository email, IPostRepository posts, ICommentRepository comments, IVoteRepository votes)
        {
            _image = image;
            _email = email;
            _comments = comments;
            _votes = votes;
            _posts = posts;
        }

        /// <summary>
        /// Retrieves all posts
        /// </summary>
        /// <returns></returns>
        public async Task Refresh()
        {
            Posts = await _posts.GetPosts();
        }

        /// <summary>
        /// Adds an up vote
        /// </summary>
        /// <param name="source"></param>
        /// <param name="postId">Post Id</param>
        /// <param name="username">Username</param>
        /// <returns>Successful completion of task</returns>
        public async Task UpVote(ComponentBase source, int postId, string username)
        {
            await _votes.UpVote(postId, username);
            NotifyStateChanged(source);
        }

        /// <summary>
        /// Adds a down vote
        /// </summary>
        /// <param name="source"></param>
        /// <param name="postId">Post Id</param>
        /// <param name="username">Username</param>
        /// <returns>Successful completion of task</returns>
        public async Task DownVote(ComponentBase source, int postId, string username)
        {
            await _votes.DownVote(postId, username);
            NotifyStateChanged(source);
        }

        /// <summary>
        /// Adds a comment
        /// </summary>
        /// <param name="source"></param>
        /// <param name="comment">Comment View Model</param>
        /// <returns>Successful completion of task</returns>
        public async Task AddComment(ComponentBase source, CommentVM comment)
        {
            this.Comment = comment;
            var newComment = await _comments.Create(comment);
            NotifyStateChanged(source);
        }

        /// <summary>
        /// Deletes a comment
        /// </summary>
        /// <param name="source"></param>
        /// <param name="id">Comment id</param>
        /// <returns>Successful completion of task</returns>
        public async Task DeleteComment(ComponentBase source, int id)
        {
            await _comments.Delete(id);
            NotifyStateChanged(source);
        }

        /// <summary>
        /// Updates a comment
        /// </summary>
        /// <param name="source"></param>
        /// <param name="comment">CommentModel</param>
        /// <returns>Successful completion of task</returns>
        public async Task UpdateComment(ComponentBase source, CommentModel comment)
        {
            await _comments.Update(comment);
            NotifyStateChanged(source);
        }

        /// <summary>
        /// Deletes a post
        /// </summary>
        /// <param name="source"></param>
        /// <param name="id">Post Id</param>
        /// <returns>Successful completion of task</returns>
        public async Task DeletePost(ComponentBase source, int id)
        {
            await _posts.Delete(id);
            NotifyStateChanged(source);
        }

        /// <summary>
        /// Updates a post
        /// </summary>
        /// <param name="source"></param>
        /// <param name="post">PostModel</param>
        /// <returns>Successful completion of task</returns>
        public async Task UpdatePost(ComponentBase source, PostModel post)
        {
            await _posts.UpdatePost(post);
            NotifyStateChanged(source);
        }

        /// <summary>
        /// Adds a post
        /// </summary>
        /// <param name="source"></param>
        /// <param name="post">PostModel</param>
        /// <param name="url">Url of Dropbox image</param>
        /// <returns>Successful completion of task</returns>
        public async Task AddPost(ComponentBase source, PostModel post, string url)
        {
            await _posts.Create(post, url);
            await _email.NewPost(url);
            NotifyStateChanged(source);
        }

        /// <summary>
        /// Uploads an image to Drobox account
        /// </summary>
        /// <param name="source"></param>
        /// <param name="name">Name of image</param>
        /// <param name="fs">Image file stream</param>
        /// <returns>URL of Dropbox image</returns>
        public async Task<string> AddImgToDropBox(ComponentBase source, string name, Stream fs)
        {
            string url = await _image.AddImgToDropBox(fs, name);
            NotifyStateChanged(source);
            return url;
        }

        /// <summary>
        /// Checks if email already exists
        /// </summary>
        /// <param name="email">String</param>
        /// <returns>bool</returns>
        public bool CheckEmail(string email)
        {
            bool exists = _email.CheckEmail(email);
            return exists;
        }

        /// <summary>
        /// Checks if username already exists
        /// </summary>
        /// <param name="username">String</param>
        /// <returns>bool</returns>
        public bool CheckUsername(string username)
        {
            bool exists = _email.CheckUsername(username);
            return exists;
        }

        private void NotifyStateChanged(ComponentBase source)
            => StateChanged?.Invoke(source);
    }
}