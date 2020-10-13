using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Models.Interfaces
{
    public interface IComment
    {
        /// <summary>
        /// Adds a comment to the database
        /// </summary>
        /// <param name="userId">Id of the user associated with the comment</param>
        /// <param name="post">Post that is being commented on</param>
        /// <param name="content">The content of the comment</param>
        /// <returns>Successful completion of task</returns>
        public Task Create(string userId, Post post, string content);

        /// <summary>
        /// Retrieves the comments for a specified post
        /// </summary>
        /// <param name="postId">The id of the post</param>
        /// <returns>All comments for post</returns>
        public Task<List<Comment>> CommentsForPost(int postId);
    }
}