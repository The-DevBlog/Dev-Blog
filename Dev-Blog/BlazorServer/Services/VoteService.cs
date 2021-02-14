using BlazorServer.Data;
using BlazorServer.Interfaces;
using BlazorServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Services
{
    public class VoteService : IVotes
    {
        private AppDbContext _db;

        public VoteService(AppDbContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Adds a vote to the database
        /// </summary>
        /// <param name="vote">The vote to add</param>
        /// <returns>Successful completion of task</returns>
        public async Task UpVote(int postId, string username)
        {
            var post = await _db.Post.Where(x => x.Id == postId)
                                     .FirstOrDefaultAsync();

            var vote = new VoteModel()
            {
                PostModelId = postId,
                UserName = username,
                Post = post
            };

            if (await HasUpVoted(postId, username))
            {
                post.UpVotes--;
                vote.UpVote = false;
            }
            else
            {
                post.UpVotes++;
                vote.UpVote = true;
            }

            _db.Entry(post).State = EntityState.Modified;
            _db.Entry(vote).State = EntityState.Added;

            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Adds a vote to the database
        /// </summary>
        /// <param name="vote">The vote to add</param>
        /// <returns>Successful completion of task</returns>
        public async Task DownVote(int postId, string username)
        {
            var post = await _db.Post.Where(x => x.Id == postId)
                                     .FirstOrDefaultAsync();

            post.DownVotes++;

            var vote = new VoteModel()
            {
                PostModelId = postId,
                UserName = username,
                DownVote = true,
                Post = post
            };

            _db.Entry(post).State = EntityState.Modified;
            _db.Entry(vote).State = EntityState.Added;

            await _db.SaveChangesAsync();
        }

        private async Task<bool> HasUpVoted(int postId, string username)
        {
            var vote = await _db.Vote.Where(x => x.PostModelId == postId &&
                                      x.UserName == username)
                               .FirstOrDefaultAsync();

            return vote.UpVote;
        }

        private async Task<bool> HasDownVoted(int postId, string username)
        {
            var vote = await _db.Vote.Where(x => x.PostModelId == postId &&
                                      x.UserName == username)
                               .FirstOrDefaultAsync();

            return vote.DownVote;
        }
    }
}