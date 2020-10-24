using Dev_Blog.Data;
using Dev_Blog.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public VoteService(AppDbContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
    }
}