using Dev_Blog.Models;
using Dev_Blog.Models.Interfaces;
using Dev_Blog.Models.ViewModels;
using Dropbox.Api.TeamLog;
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
        public async Task<string> PostComment(CommentVM comment)
        {
            // if comment is greater than 750 characters
            if (comment.Content.Length >= 750)
            {
                //string[] result = new

                string[] result = new string[]
                {
                    "Comment length must be 750 characters or less.",
                    "unsuccessful"
                };
                return JsonConvert.SerializeObject(result);
            }

            Comment newComment = await _comment.Create(new Comment
            {
                PostId = comment.PostId,
                Content = comment.Content,
                UserName = HttpContext.User.Identity.Name,
                UserId = _userManager.GetUserId(User)
            });

            Object[] json = { newComment, newComment.Date.ToString("MM/dd/yyyy hh:mm tt") };
            return JsonConvert.SerializeObject(json);
        }

        [HttpGet("/GetComments")]
        public async Task<string> GetComments()
        {
            return JsonConvert.SerializeObject(await _comment.GetAllComments());
        }

        [HttpPost("/UpVote")]
        public async Task<string> UpVote(Vote vote)
        {
            Post post = await _post.GetPost(vote.PostId);
            string userId = _userManager.GetUserId(User);

            vote.UserId = userId;

            Vote downVote = new Vote
            {
                PostId = vote.PostId,
                UserId = userId,
            };

            // check to see if a user has previously voted
            var hasUpVoted = await _vote.HasUpVoted(vote);
            var hasDownVoted = await _vote.HasDownVoted(downVote);

            if (hasDownVoted)
                await _vote.DeleteVote(downVote);

            if (hasUpVoted)
                await _vote.DeleteVote(vote);
            else
                await _vote.CreateUpVote(vote);

            int[] voteCount = { post.UpVotes, post.DownVotes };
            return JsonConvert.SerializeObject(voteCount);
        }

        [HttpPost("/DownVote")]
        public async Task<string> DownVote(Vote vote)
        {
            Post post = await _post.GetPost(vote.PostId);
            string userId = _userManager.GetUserId(User);

            vote.UserId = userId;

            Vote upVote = new Vote
            {
                PostId = vote.PostId,
                UserId = userId
            };

            // check to see if a user has previously voted
            bool hasDownVoted = await _vote.HasDownVoted(vote);
            var hasUpVoted = await _vote.HasUpVoted(upVote);

            if (hasUpVoted)
                await _vote.DeleteVote(upVote);

            if (hasDownVoted)
                await _vote.DeleteVote(vote);
            else
                await _vote.CreateDownVote(vote);

            int[] voteCount = { post.UpVotes, post.DownVotes };
            return JsonConvert.SerializeObject(voteCount);
        }
    }
}