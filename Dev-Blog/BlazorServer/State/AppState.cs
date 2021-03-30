﻿using BlazorServer.Data;
using BlazorServer.Interfaces;
using BlazorServer.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static BlazorServer.Pages.Posts;

namespace BlazorServer.State
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

        public async Task Refresh()
        {
            Posts = await _posts.GetPosts();
        }

        public async Task UpVote(ComponentBase source, int postId, string username)
        {
            await _votes.UpVote(postId, username);
            NotifyStateChanged(source);
        }

        public async Task DownVote(ComponentBase source, int postId, string username)
        {
            await _votes.DownVote(postId, username);
            NotifyStateChanged(source);
        }

        public async Task AddComment(ComponentBase source, CommentVM comment)
        {
            this.Comment = comment;
            var newComment = await _comments.Create(comment);
            NotifyStateChanged(source);
        }

        public async Task DeleteComment(ComponentBase source, int id)
        {
            await _comments.Delete(id);
            NotifyStateChanged(source);
        }

        public async Task UpdateComment(ComponentBase source, CommentModel comment)
        {
            await _comments.Update(comment);
            NotifyStateChanged(source);
        }

        public async Task DeletePost(ComponentBase source, int id)
        {
            await _posts.Delete(id);
            NotifyStateChanged(source);
        }

        public async Task UpdatePost(ComponentBase source, PostModel post)
        {
            await _posts.UpdatePost(post);
            NotifyStateChanged(source);
        }

        public async Task AddPost(ComponentBase source, PostModel post, string url)
        {
            await _posts.Create(post, url);
            await _email.NewPost(url);
            NotifyStateChanged(source);
        }

        public async Task<string> AddImgToDropBox(ComponentBase source, string name, Stream fs)
        {
            string url = await _image.AddImgToDropBox(fs, name);
            NotifyStateChanged(source);
            return url;
        }

        public async Task<bool> CheckEmail(string email)
        {
            bool exists = await _email.CheckEmail(email);
            return exists;
        }

        public async Task<bool> CheckUsername(string username)
        {
            bool exists = await _email.CheckUsername(username);
            return exists;
        }

        private void NotifyStateChanged(ComponentBase source)
            => StateChanged?.Invoke(source);
    }
}