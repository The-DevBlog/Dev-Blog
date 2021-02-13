using BlazorServer.Data;
using BlazorServer.Interfaces;
using BlazorServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BlazorServer.Pages.Posts;

namespace BlazorServer.Services
{
    public class CommentService : IComments
    {
        private readonly AppDbContext _db;

        public CommentService(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Adds a comment to the database
        /// </summary>
        /// <param name="comment">The comment to add</param>
        /// <returns>CommentModel</returns>
        public async Task<CommentModel> Create(CommentVM comment)
        {
            CommentModel newComment = new CommentModel()
            {
                UserName = comment.UserName,
                PostModelId = comment.PostModelId,
                Content = comment.Content
            };

            _db.Entry(newComment).State = EntityState.Added;
            await _db.SaveChangesAsync();

            return newComment;
        }

        /// <summary>
        /// Retrieves all comments from database
        /// </summary>
        /// <returns>List<CommentModel></returns>
        public async Task<List<CommentModel>> GetComments()
        {
            var comments = await _db.Comment.OrderByDescending(x => x.Date)
                                            .ToListAsync();

            return comments;
        }

        public async Task<CommentModel> GetComment(int commentId)
        {
            var comment = await _db.Comment.Where(c => c.Id == commentId)
                                           .FirstOrDefaultAsync();

            return comment;
        }

        public async Task Delete(int commentId)
        {
            var comment = await GetComment(commentId);
            _db.Entry(comment).State = EntityState.Deleted;
            await _db.SaveChangesAsync();
        }
    }
}