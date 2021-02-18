using BlazorServer.Data;
using BlazorServer.Interfaces;
using BlazorServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BlazorServer.Pages.Posts;

namespace BlazorServer.State
{
    public class AppState
    {
        public event Action OnCommentChange;

        public event Action OnUpVoteChange;

        public event Action OnDownVoteChange;

        public event Action OnPostChange;

        public List<PostModel> Posts { get; set; }
        private readonly ICommentRepository _comments;
        private readonly IPostRepository _posts;
        private readonly IVoteRepository _votes;
        private readonly IDbContextFactory<AppDbContext> DbFactory;

        public AppState(IPostRepository posts, ICommentRepository comments, IVoteRepository votes, IDbContextFactory<AppDbContext> db)
        {
            _comments = comments;
            _votes = votes;
            _posts = posts;
            DbFactory = db;
        }

        //TODO: refreshAllPosts()
        public async Task Refresh()
        {
            //Posts = await _posts.GetPosts();
        }

        public async Task AddUpVote(int postId, string username)
        {
            var downVote = await _votes.GetDownVote(postId, username);
            var upVote = await _votes.UpVote(postId, username);

            if (downVote != null)
            {
                Posts.Where(p => p.Id == postId)
                     .FirstOrDefault()
                     .DownVotes.Remove(downVote);
            }

            if (upVote != null)
            {
                Posts.Where(p => p.Id == postId)
                     .FirstOrDefault()
                     .UpVotes.Remove(upVote);
            }
            else
            {
                Posts.Where(p => p.Id == postId)
                     .FirstOrDefault()
                     .UpVotes.Add(upVote);
            }

            OnUpVoteChange?.Invoke();
        }

        public async Task AddDownVote(int postId, string username)
        {
            var upVote = await _votes.GetUpVote(postId, username);
            var downVote = await _votes.DownVote(postId, username);

            if (upVote != null)
            {
                Posts.Where(p => p.Id == postId)
                     .FirstOrDefault()
                     .UpVotes.Remove(upVote);
            }

            if (downVote != null)
            {
                Posts.Where(p => p.Id == postId)
                     .FirstOrDefault()
                     .DownVotes.Remove(downVote);
            }
            else
            {
                Posts.Where(p => p.Id == postId)
                     .FirstOrDefault()
                     .DownVotes.Add(downVote);
            }

            OnDownVoteChange?.Invoke();
        }

        public async Task AddPost(PostModel post, string url)
        {
            //using var ctx = DbFactory.CreateDbContext();
            //post.ImgURL = url;

            //var newp = ctx.Post.Add(post).Entity;
            //await ctx.SaveChangesAsync();

            var newPost = await _posts.Create(post, url);
            Posts.Add(newPost);
            OnPostChange?.Invoke();
        }

        public async Task AddComment(CommentVM comment)
        {
            //using var ctx = DbFactory.CreateDbContext();

            //var comm = new CommentModel()
            //{
            //    UserName = comment.UserName,
            //    Content = comment.Content,
            //    PostModelId = postId
            //};

            //var newC = ctx.Comment.Add(comm).Entity;

            //await ctx.SaveChangesAsync();
            var newComment = await _comments.Create(comment);

            Posts.Where(p => p.Id == comment.PostModelId).FirstOrDefault()
                 .Comments.Add(newComment);

            OnCommentChange?.Invoke();
        }
    }
}