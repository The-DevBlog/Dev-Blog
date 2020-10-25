using Dev_Blog.Data;
using Dev_Blog.Models.Interfaces;
using Dropbox.Api.CloudDocs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            List<Comment> comments = await _context.Comment.OrderByDescending(x => x.Date).Include(x => x.Post)
                .ToListAsync();
            return comments;
        }

        /// <summary>
        /// Retrieves a specified comment from the database
        /// </summary>
        /// <param name="id">Specified id of comment</param>
        /// <returns>Specified comment</returns>
        public async Task<Comment> GetComment(int id)
        {
            return await _context.Comment.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Retrieves the latest comment in the database
        /// </summary>
        /// <returns>The latest comment</returns>
        public async Task<Comment> GetLatestComment()
        {
            return await _context.Comment.LastOrDefaultAsync();
        }

        /// <summary>
        /// Removes a specified comment from the database
        /// </summary>
        /// <param name="comment">Specified comment to delete</param>
        /// <returns>Void</returns>
        public async Task Delete(Comment comment)
        {
            _context.Entry(comment).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}