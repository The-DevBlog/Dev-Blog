using Dev_Blog.Data;
using Dev_Blog.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
        /// Adds an upvote to the database
        /// </summary>
        /// <param name="vote">The vote to add</param>
        /// <returns>Successful completion of task</returns>
        public async Task CreateUpVote(UpVote vote)
        {
            Post post = await _context.Post.Where(x => x.Id == vote.PostId).FirstOrDefaultAsync();
            post.UpVotes++;
            vote.Post = post;

            _context.Entry(post).State = EntityState.Modified;
            _context.Entry(vote).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Adds an downvote to the database
        /// </summary>
        /// <param name="vote">The vote to add</param>
        /// <returns>Successful completion of task</returns>
        public async Task CreateDownVote(DownVote vote)
        {
            Post post = await _context.Post.Where(x => x.Id == vote.PostId).FirstOrDefaultAsync();
            post.DownVotes++;
            vote.Post = post;

            _context.Entry(post).State = EntityState.Modified;
            _context.Entry(vote).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves a specified upvote from the database
        /// </summary>
        /// <param name="vote">The specified vote</param>
        /// <returns>The specified vote</returns>
        public async Task<UpVote> GetUpVote(UpVote vote)
        {
            return await _context.UpVote.Where(x => x == vote).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieves a specified downvote from the database
        /// </summary>
        /// <param name="vote">The specified vote</param>
        /// <returns>The specified vote</returns>
        public async Task<DownVote> GetDownVote(DownVote vote)
        {
            return await _context.DownVote.Where(x => x == vote).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Removes a specified downvote from the database
        /// </summary>
        /// <param name="vote">The vote to remove</param>
        /// <returns>Successful completion of task</returns>
        public async Task DeleteDownVote(DownVote vote)
        {
            DownVote downVote = await GetDownVote(vote);
            Post post = await _context.Post.Where(x => x.Id == vote.PostId).FirstOrDefaultAsync();
            post.DownVotes--;

            _context.Entry(post).State = EntityState.Modified;
            _context.Entry(downVote).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a specified upvote from the database
        /// </summary>
        /// <param name="vote">The vote to remove</param>
        /// <returns>Successful completion of task</returns>s
        public async Task DeleteUpVote(UpVote vote)
        {
            UpVote upVote = await GetUpVote(vote);
            Post post = await _context.Post.Where(x => x.Id == vote.PostId).FirstOrDefaultAsync();
            post.UpVotes--;

            _context.Entry(post).State = EntityState.Modified;
            _context.Entry(upVote).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks to see if the user has already upvoted on a specified post
        /// </summary>
        /// <param name="vote">The vote to check for</param>
        /// <returns>A boolean indicating whether the vote exists within the database</returns>
        public async Task<bool> HasUpVoted(UpVote vote)
        {
            var result = await _context.UpVote.Where(x => x == vote).FirstOrDefaultAsync();
            return result != null ? true : false;
        }

        /// <summary>
        /// Checks to see if the user has already downvoted on a specified post
        /// </summary>
        /// <param name="vote">The vote to check for</param>
        /// <returns>A boolean indicating whether the vote exists within the database</returns>
        public async Task<bool> HasDownVoted(DownVote vote)
        {
            var result = await _context.DownVote.Where(x => x == vote).FirstOrDefaultAsync();
            return result != null ? true : false;
        }
    }
}