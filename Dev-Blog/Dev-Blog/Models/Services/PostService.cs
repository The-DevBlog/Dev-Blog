using Dev_Blog.Data;
using Dev_Blog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Dev_Blog.Models.Services
{
    public class PostService : IPost
    {
        private AppDbContext _context;

        public PostService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new post to the database
        /// </summary>
        /// <param name="post">The new post</param>
        /// <param name="url">The url of the image</param>
        /// <returns>New post</returns>
        public async Task<Post> Create(Post post, string url)
        {
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
            return await _context.Post.Include(x => x.Comments).OrderByDescending(x => x.Date).ToListAsync();
        }

        /// <summary>
        /// Gets the most recent post
        /// </summary>
        /// <returns>Most recent post</returns>
        public async Task<Post> GetLatestPost()
        {
            return await _context.Post.Include(x => x.Comments).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieves a specified post
        /// </summary>
        /// <param name="postId">Id of specified post</param>
        /// <returns>Specified post</returns>
        public async Task<Post> GetPost(int postId)
        {
            return await _context.Post.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == postId);
        }

        /// <summary>
        /// Removes a specified post from the database
        /// </summary>
        /// <param name="post"></param>
        /// <returns>Successful completion of task</returns>
        public async Task Delete(Post post)
        {
            _context.Entry(post).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}