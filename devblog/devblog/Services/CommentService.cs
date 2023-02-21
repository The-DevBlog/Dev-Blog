using devblog.Data;
using devblog.Interfaces;
using devblog.Models;
using Microsoft.EntityFrameworkCore;

namespace devblog.Services
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _db;

        public CommentService(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Creates a new comment
        /// </summary>
        /// <param name="content">The content of the comment</param>
        /// <param name="username">username of comment</param>
        /// <param name="postId">postId of comment</param>
        /// <returns>Comment</returns>
        public async Task<Comment> Create(string content, string username, int postId)
        {
            Comment newComment = new Comment()
            {
                UserName = username,
                PostId = postId,
                Content = content
            };

            _db.Comment.Add(newComment);
            await _db.SaveChangesAsync();

            return newComment;
        }

        /// <summary>
        /// Updates a specified comment
        /// </summary>
        /// <param name="comment">Comment Model</param>
        /// <returns>Successful completion of task</returns>
        public async Task Update(Comment comment)
        {
            _db.Comment.Update(comment);
            await _db.SaveChangesAsync();
        }

        // /// <summary>
        // /// Retrieves a specified comment
        // /// </summary>
        // /// <param name="commentId">Comment Id</param>
        // /// <returns>Comment</returns>
        // public async Task<Comment> GetComment(int commentId)
        // {
        //     var comment = await _db.Comment.Where(c => c.Id == commentId)
        //                                    .FirstOrDefaultAsync();
        //     return comment;
        // }

        /// <summary>
        /// Deletes a specified comment
        /// </summary>
        /// <param name="commentId">Comment Id</param>
        /// <returns>Successful completion of task</returns>
        // public async Task Delete(int commentId)
        // {
        //     var comment = await GetComment(commentId);
        //     _db.Remove(comment);
        //     await _db.SaveChangesAsync();
        // }
    }
}