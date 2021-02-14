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
            if (await HasDownVoted(postId, username))
            {
                var vote = await _db.DownVote.Where(v => v.PostModelId == postId &&
                                          v.UserName == username)
                                             .FirstOrDefaultAsync();

                _db.Entry(vote).State = EntityState.Deleted;
            }
            else if (await HasUpVoted(postId, username))
            {
                var vote = await _db.UpVote.Where(v => v.PostModelId == postId &&
                                          v.UserName == username)
                                           .FirstOrDefaultAsync();

                _db.Entry(vote).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                return;
            }

            var newVote = new UpVoteModel()
            {
                PostModelId = postId,
                UserName = username
            };

            _db.Entry(newVote).State = EntityState.Added;
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Adds a vote to the database
        /// </summary>
        /// <param name="vote">The vote to add</param>
        /// <returns>Successful completion of task</returns>
        public async Task DownVote(int postId, string username)
        {
            if (await HasUpVoted(postId, username))
            {
                var vote = await _db.UpVote.Where(v => v.PostModelId == postId &&
                          v.UserName == username)
                                           .FirstOrDefaultAsync();

                _db.Entry(vote).State = EntityState.Deleted;
            }
            else if (await HasDownVoted(postId, username))
            {
                var vote = await _db.DownVote.Where(v => v.PostModelId == postId &&
                                          v.UserName == username)
                                             .FirstOrDefaultAsync();

                _db.Entry(vote).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                return;
            }
            var newVote = new DownVoteModel()
            {
                PostModelId = postId,
                UserName = username
            };

            _db.Entry(newVote).State = EntityState.Added;
            await _db.SaveChangesAsync();
        }

        private async Task<bool> HasUpVoted(int postId, string username)
        {
            var vote = await _db.UpVote.Where(x => x.PostModelId == postId &&
                                      x.UserName == username)
                                       .FirstOrDefaultAsync();

            return vote == null ? false : true;
        }

        private async Task<bool> HasDownVoted(int postId, string username)
        {
            var vote = await _db.DownVote.Where(x => x.PostModelId == postId &&
                                      x.UserName == username)
                                         .FirstOrDefaultAsync();

            return vote == null ? false : true;
        }
    }
}