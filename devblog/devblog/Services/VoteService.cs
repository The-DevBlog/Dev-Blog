using devblog.Data;
using devblog.Interfaces;
using devblog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace devblog.Services
{
    public class VoteService : IVoteService
    {
        private readonly AppDbContext _db;

        public VoteService(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Returns the number of upvotes for specific post
        /// </summary>
        /// <param name="id">id of post</param>
        /// <returns>number of upvotes</returns>
        public async Task<int> GetUpVotesForPost(int id)
        {
            var votes = _db.UpVote.Where(x => x.PostId == id).Count();
            return votes;
        }

        /// <summary>
        /// Returns the number of downvotes for specific post
        /// </summary>
        /// <param name="id">id of post</param>
        /// <returns>number of downvotes</returns>
        public async Task<int> GetDownVotesForPost(int id)
        {
            var votes = _db.DownVote.Where(x => x.PostId == id).Count();
            return votes;
        }

        /// <summary>
        /// Adds an up vote
        /// </summary>
        /// <param name="vote"></param>
        /// <returns>Number of votes for post</returns>
        public async Task<int> UpVote(int postId, string username)
        {
            var downVote = await _db.DownVote.Where(x => x.PostId == postId &&
                          x.UserName == username)
                           .FirstOrDefaultAsync();

            var upVote = await _db.UpVote.Where(v => v.PostId == postId &&
                          v.UserName == username)
                           .FirstOrDefaultAsync();

            // remove the downvote if there is already a downvote. "Unlike"
            if (downVote != null)
                _db.Remove(downVote);
            // remove upvote if there is an upvote
            else if (upVote != null)
            {
                _db.Remove(upVote);
                await _db.SaveChangesAsync();
                return _db.UpVote.Where(x => x.PostId == postId).Count(); ;
            }

            var newVote = new UpVote()
            {
                PostId = postId,
                UserName = username
            };

            _db.Add(newVote);
            await _db.SaveChangesAsync();

            int totalVotes = _db.UpVote.Where(x => x.PostId == postId).Count();
            return totalVotes;
        }

        /// <summary>
        /// Adds a down vote
        /// </summary>
        /// <param name="vote"></param>
        /// <returns>Number of votes for post</returns>
        public async Task<int> DownVote(int postId, string username)
        {
            var downVote = await _db.DownVote.Where(x => x.PostId == postId &&
                          x.UserName == username)
                           .FirstOrDefaultAsync();

            var upVote = await _db.UpVote.Where(v => v.PostId == postId &&
                          v.UserName == username)
                           .FirstOrDefaultAsync();

            // remove the upvote if there is already a downvote. "Un-dislike"
            if (upVote != null)
                _db.Remove(upVote);
            // remove downvote if there is an upvote
            else if (downVote != null)
            {
                _db.Remove(downVote);
                await _db.SaveChangesAsync();
                return _db.DownVote.Where(x => x.PostId == postId).Count(); ;
            }
            var newVote = new DownVote()
            {
                PostId = postId,
                UserName = username
            };

            _db.Add(newVote);
            await _db.SaveChangesAsync();

            int totalVotes = _db.DownVote.Where(x => x.PostId == postId).Count();
            return totalVotes;
        }
    }
}