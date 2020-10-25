using Dev_Blog.Models;
using Dev_Blog.Models.Interfaces;
using Dev_Blog.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Controllers
{
    [ApiController]
    public class AjaxController : ControllerBase
    {
        private readonly IComment _comment;
        private readonly IPost _post;
        private readonly IVote _vote;
        private readonly UserManager<User> _userManager;

        public AjaxController(IVote vote, IPost post, IComment comment, UserManager<User> userManager)
        {
            _vote = vote;
            _post = post;
            _userManager = userManager;
            _comment = comment;
        }

        [HttpPost("/PostComment")]
        public async Task<string> PostComment(Comment comment)
        {
            Post post = await _post.GetPost(comment.PostId);
            string id = _userManager.GetUserId(User);
            string userName = HttpContext.User.Identity.Name;
            Comment newComment = await _comment.Create(id, post, comment.Content, userName);

            // convert newly created comment into json string
            CommentVM jsonComment = new CommentVM
            {
                PostId = newComment.PostId,
                Content = newComment.Content,
                UserName = newComment.UserName,
                Date = newComment.Date.ToString("MM/dd/yyyy hh:mm tt")
            };

            string json = JsonConvert.SerializeObject(jsonComment);
            return json;
        }

        [HttpGet("/GetComments")]
        public async Task<string> GetComments()
        {
            List<CommentVM> jsonComments = new List<CommentVM>();

            List<Comment> comments = await _comment.GetAllComments();

            // convert all comments into json strings
            foreach (Comment item in comments)
            {
                CommentVM jsonComment = new CommentVM
                {
                    PostId = item.PostId,
                    Content = item.Content,
                    UserName = item.UserName,
                    Date = item.Date.ToString("MM/dd/yyyy hh:mm tt")
                };

                jsonComments.Add(jsonComment);
            }

            string json = JsonConvert.SerializeObject(jsonComments);
            return json;
        }

        [HttpPost("/UpVote")]
        public async Task<string> UpVote(UpVote vote)
        {
            Post post = await _post.GetPost(vote.PostId);
            string userId = _userManager.GetUserId(User);

            vote.UserId = userId;

            var hasUpVoted = await _vote.HasUpVoted(vote);

            // check to see if use has previously downvoted
            DownVote downVote = new DownVote
            {
                PostId = vote.PostId,
                UserId = userId
            };

            var hasDownVoted = await _vote.HasDownVoted(downVote);

            if (hasDownVoted)
                await _vote.DeleteDownVote(downVote);

            if (hasUpVoted)
                await _vote.DeleteUpVote(vote);
            else
                await _vote.CreateUpVote(vote);

            int[] voteCount = { post.UpVotes, post.DownVotes };
            string json = JsonConvert.SerializeObject(voteCount);
            return json;
        }

        [HttpPost("/DownVote")]
        public async Task<string> DownVote(DownVote vote)
        {
            Post post = await _post.GetPost(vote.PostId);
            string userId = _userManager.GetUserId(User);

            vote.UserId = userId;

            bool hasDownVoted = await _vote.HasDownVoted(vote);

            // check to see if use has previously downvoted
            UpVote upVote = new UpVote
            {
                PostId = vote.PostId,
                UserId = userId
            };

            var hasUpVoted = await _vote.HasUpVoted(upVote);

            if (hasUpVoted)
                await _vote.DeleteUpVote(upVote);

            if (hasDownVoted)
                await _vote.DeleteDownVote(vote);
            else
                await _vote.CreateDownVote(vote);

            int[] voteCount = { post.UpVotes, post.DownVotes };
            string json = JsonConvert.SerializeObject(voteCount);
            return json;
        }
    }
}