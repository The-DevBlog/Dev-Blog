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
    public class PostService : IPosts
    {
        private AppDbContext _db;

        public PostService(AppDbContext context)
        {
            _db = context;
        }

        public async Task<List<PostModel>> GetPosts()
        {
            return await _db.Post.ToListAsync();
        }
    }
}