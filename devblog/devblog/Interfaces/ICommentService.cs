using devblog.Models;

namespace devblog.Interfaces
{
    public interface ICommentService
    {
        /// <summary>
        /// Creates a new comment
        /// </summary>
        /// <param name="content">The content of the comment</param>
        /// <param name="userName">userName of comment</param>
        /// <param name="postId">postId of comment</param>
        /// <returns>Comment</returns>
        Task<Comment> Create(string content, string userName, int postId);

        /// <summary>
        /// Update a comment
        /// </summary>
        /// <param name="id">id of comment to update</param>
        /// <param name="content">new content of comment</param>
        /// <returns></returns>
        Task<Comment> Update(int id, string content);

        /// <summary>
        /// Retrieves a specified comment
        /// </summary>
        /// <param name="commentId">Comment Id</param>
        /// <returns>Comment</returns>
        Task<Comment> Get(int id);

        /// <summary>
        /// Deletes a specified comment
        /// </summary>
        /// <param name="commentId">Comment Id</param>
        /// <returns>Successful completion of task</returns>
        Task Delete(int id);
    }
}
