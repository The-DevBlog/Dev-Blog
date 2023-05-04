using devblog.Models;

namespace devblog.Interfaces
{
    public interface IPostService
    {
        /// <summary>
        /// Creates a new post
        /// </summary>
        /// <param name="description">Description of post</param>
        /// <param name="file">File to uploadfcgvvb</param>
        /// <returns>Post</returns>
        Task<Post> Create(string description, IFormFile[] files);

        /// <summary>
        /// Retrieves all posts
        /// </summary>
        /// <returns>List<Post></returns>
        Task<List<Post>> Get();

        /// <summary>
        /// Retrieves a specified post
        /// </summary>
        /// <param name="postId">Post Id</param>
        /// <returns>Post</returns>
        Task<Post> Get(int postId);

        /// <summary>
        /// Updates a post
        /// </summary>
        /// <param name="post"></param>
        /// <returns>Successful completion of task</returns>
        Task Update(Post post);

        /// <summary>
        /// Removes a specified post
        /// </summary>
        /// <param name="postId">Post Id</param>
        /// <returns>Successful completion of task</returns>
        Task Delete(int postId);
    }
}