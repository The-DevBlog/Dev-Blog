using Dev_Blog.Data;
using Dev_Blog.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task Create(Vote vote)
        {
            _context.Entry(vote).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Vote vote)
        {
            _context.Entry(vote).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}