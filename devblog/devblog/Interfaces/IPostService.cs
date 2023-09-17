﻿using devblog.Controllers;
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