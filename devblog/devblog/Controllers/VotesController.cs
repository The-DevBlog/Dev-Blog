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
        /// Returns the number of upvotes for specific post
        /// </summary>
        /// <param name="id">id of post</param>
        /// <returns>number of upvotes</returns>
        [HttpGet("{id}/upvotes")]
        public async Task<int> GetUpVotesForPost(int id)
        {
            var votes = await _votes.GetUpVotesForPost(id);
            return votes;
        }

        /// <summary>
        /// Returns the number of downvotes for specific post
        /// </summary>
        /// <param name="id">id of post</param>
        /// <returns>number of downvotes</returns>
        [HttpGet("{id}/downvotes")]
        public async Task<int> GetDownVotesForPost(int id)
        {
            var votes = await _votes.GetDownVotesForPost(id);
            return votes;
        }

        /// <summary>
        /// Adds an up vote
        /// </summary>
        /// <param name="vote"></param>
        /// <returns>Number of votes for post</returns>
        [Authorize]
        [HttpPost("{id}/upvote")]
        public async Task<int> UpVote(int id, [FromBody] string username)
        {
            var vote = await _votes.UpVote(id, username);
            return vote;
        }

        /// <summary>
        /// Adds a down vote
        /// </summary>
        /// <param name="vote"></param>
        /// <returns>Number of votes for post</returns>
        [Authorize]
        [HttpPost("{id}/downvote")]
        public async Task<int> DownVote(int id, [FromBody] string username)
        {
            var vote = await _votes.DownVote(id, username);
            return vote;
        }
    }
}

