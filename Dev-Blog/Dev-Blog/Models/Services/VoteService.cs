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

        public async Task CreateUpVote(UpVote vote)
        {
            Post post = await _context.Post.Where(x => x.Id == vote.PostId).FirstOrDefaultAsync();
            post.UpVotes++;
            vote.Post = post;

            _context.Entry(post).State = EntityState.Modified;
            _context.Entry(vote).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }

        public async Task CreateDownVote(DownVote vote)
        {
            Post post = await _context.Post.Where(x => x.Id == vote.PostId).FirstOrDefaultAsync();
            post.DownVotes++;
            vote.Post = post;

            _context.Entry(post).State = EntityState.Modified;
            _context.Entry(vote).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteDownVote(DownVote vote)
        {
            Post post = await _context.Post.Where(x => x.Id == vote.PostId).FirstOrDefaultAsync();
            post.DownVotes--;

            _context.Entry(post).State = EntityState.Modified;
            _context.Entry(vote).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUpVote(UpVote vote)
        {
            Post post = await _context.Post.Where(x => x.Id == vote.PostId).FirstOrDefaultAsync();
            post.UpVotes--;

            _context.Entry(post).State = EntityState.Modified;
            _context.Entry(vote).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasUpVoted(UpVote vote)
        {
            bool exists = _context.UpVote.ContainsAsync(vote).Result;
            return exists;
        }

        public async Task<bool> HasDownVoted(DownVote vote)
        {
            bool exists = _context.DownVote.ContainsAsync(vote).Result;
            return exists;
        }
    }
}