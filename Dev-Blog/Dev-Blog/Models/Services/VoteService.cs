using Dev_Blog.Data;
using Dev_Blog.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Models.Services
{
    public class VoteService : IVote
    {
        private AppDbContext _context;

        public VoteService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a vote to the database
        /// </summary>
        /// <param name="vote">The vote to add</param>
        /// <returns>Successful completion of task</returns>
        public async Task CreateUpVote(Vote vote)
        {
            Post post = await _context.Post.Where(x => x.Id == vote.PostId).FirstOrDefaultAsync();
            post.UpVotes++;
            vote.Post = post;
            vote.UpVote = true;

            _context.Entry(post).State = EntityState.Modified;
            _context.Entry(vote).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Adds aa vote to the database
        /// </summary>
        /// <param name="vote">The vote to add</param>
        /// <returns>Successful completion of task</returns>
        public async Task CreateDownVote(Vote vote)
        {
            Post post = await _context.Post.Where(x => x.Id == vote.PostId).FirstOrDefaultAsync();
            post.DownVotes++;
            vote.Post = post;
            vote.DownVote = true;

            _context.Entry(post).State = EntityState.Modified;
            _context.Entry(vote).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves a specified vote from the database
        /// </summary>
        /// <param name="vote">The specified vote</param>
        /// <returns>The specified vote</returns>
        public async Task<Vote> GetVote(Vote vote)
        {
            return await _context.Vote.Where(x => x == vote).FirstOrDefaultAsync();
        }

        ///// <summary>
        ///// Removes a specified vote from the database
        ///// </summary>
        ///// <param name="vote">The vote to remove</param>
        ///// <returns>Successful completion of task</returns>
        public async Task DeleteVote(Vote vote)
        {
            Vote result = await GetVote(vote);
            Post post = await _context.Post.Where(x => x.Id == vote.PostId).FirstOrDefaultAsync();

            if (result.UpVote) post.UpVotes--;
            else post.DownVotes--;

            _context.Entry(post).State = EntityState.Modified;
            _context.Entry(result).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks to see if the user has already upvoted on a specified post
        /// </summary>
        /// <param name="vote">The vote to check for</param>
        /// <returns>A boolean indicating whether the vote exists within the database</returns>
        public async Task<bool> HasUpVoted(Vote vote)
        {
            var result = await _context.Vote.Where(x => x == vote).FirstOrDefaultAsync();

            if (result != null) return result.UpVote;
            else return false;
        }

        /// <summary>
        /// Checks to see if the user has already downvoted on a specified post
        /// </summary>
        /// <param name="vote">The vote to check for</param>
        /// <returns>A boolean indicating whether the vote exists within the database</returns>
        public async Task<bool> HasDownVoted(Vote vote)
        {
            var result = await _context.Vote.Where(x => x == vote).FirstOrDefaultAsync();

            if (result != null) return result.DownVote;
            else return false;
        }
    }
}