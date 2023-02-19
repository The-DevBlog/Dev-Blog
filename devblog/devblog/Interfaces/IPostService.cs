using devblog.Models;

namespace devblog.Interfaces
{
    public interface IPostService
    {
        /// <summary>
        /// Adds a new post
        /// </summary>
        /// <param name="post"></param>
        /// <param name="url">The url of the post's image</param>
        /// <returns>PostModel</returns>
        Task<PostModel> Create(PostModel post, string url);

        /// <summary>
        /// Retrieves all posts
        /// </summary>
        /// <returns>List<PostModel></returns>
        Task<List<PostModel>> Get();

        /// <summary>
        /// Retrieves a specified post
        /// </summary>
        /// <param name="postId">Post Id</param>
        /// <returns>PostModel</returns>
        Task<PostModel> Get(int postId);

        /// <summary>
        /// Updates a post
        /// </summary>
        /// <param name="post"></param>
        /// <returns>Successful completion of task</returns>
        Task Update(PostModel post);

        /// <summary>
        /// Removes a specified post
        /// </summary>
        /// <param name="postId">Post Id</param>
        /// <returns>Successful completion of task</returns>
        Task Delete(int postId);
    }
}