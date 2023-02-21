using devblog.Models;

namespace devblog.Interfaces
{
    public interface ICommentService
    {
        /// <summary>
        /// Creates a new comment
        /// </summary>
        /// <param name="content">The content of the comment</param>
        /// <param name="username">username of comment</param>
        /// <param name="postId">postId of comment</param>
        /// <returns>Comment</returns>
        Task<Comment> Create(string content, string username, int postId);

        /// <summary>
        /// Updates a specified comment
        /// </summary>
        /// <param name="comment">Comment Model</param>
        /// <returns>Successful completion of task</returns>
        Task Update(Comment comment);

        /// <summary>
        /// Retrieves a specified comment
        /// </summary>
        /// <param name="commentId">Comment Id</param>
        /// <returns>Comment</returns>
        // Task<Comment> GetComment(int id);

        /// <summary>
        /// Deletes a specified comment
        /// </summary>
        /// <param name="commentId">Comment Id</param>
        /// <returns>Successful completion of task</returns>
        // Task Delete(int id);
    }
}
