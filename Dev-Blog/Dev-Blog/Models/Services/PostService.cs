using Dev_Blog.Data;
using Dev_Blog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Dev_Blog.Models.Services
{
    public class PostService : IPost
    {
        private DevBlogDbContext _context;

        public PostService(DevBlogDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new post to the database
        /// </summary>
        /// <param name="post">The new post</param>
        /// <param name="imgName">Name of the image being uploaded</param>
        /// <returns>New post</returns>
        public async Task<Post> Create(Post post, string imgName)
        {
            string url = $"https://thedevblog.blob.core.windows.net/pictures/{imgName}";

            post.ImgURL = url;
            post.Date = DateTime.Now;

            _context.Entry(post).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return post;
        }

        /// <summary>
        /// Get a list of all posts
        /// </summary>
        /// <returns>Successful result with list of posts</returns>
        public async Task<List<Post>> GetAllPosts()
        {
            List<Post> posts = await _context.Post.OrderByDescending(x => x.Date).ToListAsync();

            return posts;
        }

        /// <summary>
        /// Gets the most recent post
        /// </summary>
        /// <returns>Most recent post</returns>
        public async Task<Post> GetLatestPost()
        {
            Post post = await _context.Post.OrderByDescending(x => x.Date).FirstOrDefaultAsync();

            return post;
        }
    }
}