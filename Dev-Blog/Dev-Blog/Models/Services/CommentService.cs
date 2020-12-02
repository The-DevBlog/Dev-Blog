using Dev_Blog.Data;
using Dev_Blog.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        /// <param name="comment">The comment to add</param>
        /// <returns>The new comment</returns>
        public async Task<Comment> Create(Comment comment)
        {
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
            List<Comment> comments = await _context.Comment.OrderByDescending(x => x.Date)
                .ToListAsync();
            return comments;
        }

        /// <summary>
        /// Returns the comment count for specified post
        /// </summary>
        /// <param name="postId">The id of specified post</param>
        /// <returns>Int</returns>
        public async Task<int> GetCount(int postId)
        {
            Post post = await _context.Post.Where(x => x.Id == postId).FirstOrDefaultAsync();
            return post.Comments.Count();
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