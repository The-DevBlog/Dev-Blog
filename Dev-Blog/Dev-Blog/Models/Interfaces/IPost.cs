using Dropbox.Api.TeamLog;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Models.Interfaces
{
    public interface IPost
    {
        /// <summary>
        /// Adds a new post to the database
        /// </summary>
        /// <param name="post">The new post</param>
        /// <param name="url">The url of the image</param>
        /// <returns>New post</returns>
        Task<Post> Create(Post post, string url);

        /// <summary>
        /// Determines if there is a next page
        /// </summary>
        /// <param name="page">The page number to get</param>
        /// <returns>A boolean</returns>
        Task<bool> CanPageRight(int page);

        /// <summary>
        /// Returns the last page of posts
        /// </summary>
        /// <returns>Int of last page</returns>
        Task<int> GetLastPage();

        /// <summary>
        /// Returns one page worth of posts (5 max)
        /// </summary>
        /// <param name="page">The page number to get</param>
        /// <returns>5 posts</returns>
        Task<List<Post>> GetPage(int page);

        /// <summary>
        /// Gets the most recent post
        /// </summary>
        /// <returns>Most recent post</returns>
        Task<Post> GetLatestPost();

        /// <summary>
        /// Returns a count of all the posts in the database
        /// </summary>
        /// <returns>Count of all posts</returns>
        Task<int> GetCount();

        /// <summary>
        /// Retrieves a specified post
        /// </summary>
        /// <param name="postId">Id of specified post</param>
        /// <returns>Specified post</returns>
        Task<Post> GetPost(int postId);

        /// <summary>
        /// Removes a specified post from the database
        /// </summary>
        /// <param name="post"></param>
        /// <returns>Successful completion of task</returns>
        public Task Delete(Post post);

        /// <summary>
        /// Modifies a specified post
        /// </summary>
        /// <param name="post">The post to modify</param>
        /// <param name="description">The new description of the post</param>
        /// <param name="updateNum">New udpate number of the post</param>
        /// <returns>The modified post</returns>
        public Task<Post> Edit(Post post, string description, string updateNum);
    }
}