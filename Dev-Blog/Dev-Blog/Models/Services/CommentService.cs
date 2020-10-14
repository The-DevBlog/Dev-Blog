using Dev_Blog.Data;
using Dev_Blog.Models.Interfaces;
using Dropbox.Api.CloudDocs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Models.Services
{
    public class CommentService : IComment
    {
        private readonly AppDbContext _context;

        public CommentService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a comment to the database
        /// </summary>
        /// <param name="userId">Id of the user associated with the comment</param>
        /// <param name="post">Post that is being commented on</param>
        /// <param name="content">The content of the comment</param>
        /// <param name="userName">Username of current user</param>
        /// <returns>Successful completion of task</returns>
        public async Task<Comment> Create(string userId, Post post, string content, string userName)
        {
            Comment comment = new Comment()
            {
                UserId = userId,
                PostId = post.Id,
                Content = content,
                Date = DateTime.Now,
                Post = post,
                UserName = userName
            };

            _context.Entry(comment).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return comment;
        }

        /// <summary>
        /// Retrieves all comments from database
        /// </summary>
        /// <returns>Returns all comments</returns>
        public async Task<List<Comment>> GetAllComments()
        {
            List<Comment> comments = await _context.Comment.Include(x => x.Post)
                .OrderBy(x => x.Date).ToListAsync();
            return comments;
        }
    }
}