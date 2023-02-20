using devblog.Models;

namespace devblog.Interfaces
{
    public interface IPostService
    {
        /// <summary>
        /// Creates a new post
        /// </summary>
        /// <param name="description">Description of post</param>
        /// <param name="imgURL">Img URL of post</param>
        /// <param name="updateNum">Update number of post</param>
        /// <returns>PostModel</returns>
        Task<PostModel> Create(string description, string imgURL, string updateNum);

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