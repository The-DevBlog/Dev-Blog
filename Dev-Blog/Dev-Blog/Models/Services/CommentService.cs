using Dev_Blog.Data;
using Dev_Blog.Models.Interfaces;
using Dropbox.Api.CloudDocs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Models.Services
{
    public class CommentService : IComment
    {
        private AppDbContext _context;

        public CommentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(string userId, int postId, string content)
        {
            Comment comment = new Comment()
            {
                UserId = userId,
                PostId = postId,
                Content = content,
                Date = DateTime.Now
            };
        }

        public async Task<List<Comment>> CommentsForPost(int postId)
        {
            List<Comment> comments = await _context.Comment.
                Where(x => x.Post.Id == postId)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            return comments;
        }
    }
}