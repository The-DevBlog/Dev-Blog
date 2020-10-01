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
        /// <param name="imgName">Name of the image being uploaded</param>
        /// <returns>New post</returns>
        Task<Post> Create(Post post, string imgName);
    }
}