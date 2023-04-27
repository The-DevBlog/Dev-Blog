using devblog.Data;
using devblog.Interfaces;
using devblog.Models;
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
        /// Adds an up vote
        /// </summary>
        /// <param name="vote"></param>
        /// <returns>UpVote</returns>
        public async Task<UpVote> UpVote(int postId, string username)
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
                return null;
            }

            var newVote = new UpVote()
            {
                PostId = postId,
                UserName = username
            };

            _db.Add(newVote);
            await _db.SaveChangesAsync();
            return newVote;
        }

        /// <summary>
        /// Adds a down vote
        /// </summary>
        /// <param name="vote"></param>
        /// <returns>DownVote</returns>
        public async Task<DownVote> DownVote(int postId, string username)
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
                return null;
            }
            var newVote = new DownVote()
            {
                PostId = postId,
                UserName = username
            };

            _db.Add(newVote);
            await _db.SaveChangesAsync();
            return newVote;
        }
    }
}