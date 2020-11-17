using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dev_Blog.Models.Interfaces
{
    public interface IComment
    {
        /// <summary>
        /// Adds a comment to the database
        /// </summary>
        /// <param name="comment">The comment to add</param>
        /// <returns>The new comment</returns>
        public Task<Comment> Create(Comment comment);

        /// <summary>
        /// Retrieves all comments from database
        /// </summary>
        /// <returns>Returns all comments</returns>
        public Task<List<Comment>> GetAllComments();

        /// <summary>
        /// Removes a specified comment from the database
        /// </summary>
        /// <param name="comment">Specified comment to delete</param>
        /// <returns>Void</returns>
        public Task Delete(Comment comment);

        /// <summary>
        /// Retrieves a specified comment from the database
        /// </summary>
        /// <param name="id">Specified id of comment</param>
        /// <returns>Specified comment</returns>
        public Task<Comment> GetComment(int id);

        /// <summary>
        /// Retrieves the latest comment in the database
        /// </summary>
        /// <returns>The latest comment</returns>
        public Task<Comment> GetLatestComment();
    }
}