using devblog.Models;

namespace devblog.Interfaces
{
    public interface ICommentService
    {
        /// <summary>
        /// Updates a specified comment
        /// </summary>
        /// <param name="comment">Comment Model</param>
        /// <returns>Successful completion of task</returns>
        Task Update(CommentModel comment);

        /// <summary>
        /// Retrieves a specified comment
        /// </summary>
        /// <param name="commentId">Comment Id</param>
        /// <returns>CommentModel</returns>
        // Task<CommentModel> GetComment(int id);

        /// <summary>
        /// Deletes a specified comment
        /// </summary>
        /// <param name="commentId">Comment Id</param>
        /// <returns>Successful completion of task</returns>
        // Task Delete(int id);
    }
}
