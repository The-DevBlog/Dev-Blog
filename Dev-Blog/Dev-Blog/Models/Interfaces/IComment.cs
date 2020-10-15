﻿using System;
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
        /// <param name="userName">Username of current user</param>
        /// <returns>Successful completion of task</returns>
        public Task<Comment> Create(string userId, Post post, string content, string userName);

        /// <summary>
        /// Retrieves all comments from database
        /// </summary>
        /// <returns>Returns all comments</returns>
        public Task<List<Comment>> GetAllComments();

        /// <summary>
        /// Removes a specified comment from the database
        /// </summary>
        /// <param name="comment">Specified comment to delete</param>
        /// <returns>Void</returns>
        public Task Delete(Comment comment);

        /// <summary>
        /// Retrieves a specified comment from the database
        /// </summary>
        /// <param name="id">Specified id of comment</param>
        /// <returns>Specified comment</returns>
        public Task<Comment> GetComment(int id);
    }
}