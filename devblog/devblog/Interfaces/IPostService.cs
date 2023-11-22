using devblog.Controllers;
using devblog.Models;

namespace devblog.Interfaces
{
    public interface IPostService
    {
        /// <summary>
        /// Creates a new post
        /// </summary>
        /// <param name="post">Data for new post</param>
        /// <returns>Post</returns>
        Task<UploadStatus> Create(PostUpload post);

        /// <summary>
        /// Gets the total page count
        /// </summary>
        /// <returns>int</returns>
        Task<int> GetPageCount();

        /// <summary>
        /// Gets the total post count
        /// </summary>
        /// <returns>int</returns>
        Task<int> GetPostCount();

        /// <summary>
        /// Retrieves all posts (5 max) for specified page
        /// </summary>
        /// <param name="pageNum">The page number to get posts from</param>
        /// <returns>List<Post></returns>
        Task<List<Post>> GetPage(int pageNum);

        /// <summary>
        /// Returns the page number of a given post
        /// </summary>
        Task<int> GetPageNumber(int postId);

        /// <summary>
        /// Retrieves a specified post
        /// </summary>
        /// <param name="postId">Post Id</param>
        /// <returns>Post</returns>
        Task<Post> Get(int postId);

        /// <summary>
        /// Updates a post
        /// </summary>
        /// <param name="id">Id of post to be updated</param>
        /// <param name="description">New description of post</param>
        /// <returns>Updated post</returns>
        Task<Post> Update(int id, string description);

        /// <summary>
        /// Removes a specified post
        /// </summary>
        /// <param name="postId">Post Id</param>
        /// <returns>Successful completion of task</returns>
        Task Delete(int postId);
    }
}