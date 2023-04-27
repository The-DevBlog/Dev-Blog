using devblog.Interfaces;
using devblog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace devblog.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class VotesController : ControllerBase
    {
        private readonly IVoteService _votes;

        public VotesController(IVoteService votes)
        {
            _votes = votes;
        }

        /// <summary>
        /// Adds an up vote
        /// </summary>
        /// <param name="vote"></param>
        /// <returns>UpVote</returns>
        //[Authorize]
        [HttpPost("{id}/upvote")]
        public async Task<UpVote> UpVote(int id, string username)
        {
            var vote = await _votes.UpVote(id, username);
            return vote;
        }

        /// <summary>
        /// Adds a down vote
        /// </summary>
        /// <param name="vote"></param>
        /// <returns>DownVote</returns>
        //[Authorize]
        [HttpPost("{id}/downvote")]
        public async Task<DownVote> DownVote(int id, string username)
        {
            var vote = await _votes.DownVote(id, "hello");
            return vote;
        }
    }
}

