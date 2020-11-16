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
            post.UpVotes = 0;
            post.DownVotes = 0;

            _context.Entry(post).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return post;
        }

        /// <summary>
        /// Modifies a specified post
        /// </summary>
        /// <param name="post">The post to modify</param>
        /// <param name="description">The new description of the post</param>
        /// <param name="updateNum">New udpate number of the post</param>
        /// <returns>The modified post</returns>
        public async Task<Post> Edit(Post post, string description, string updateNum)
        {
            post.Description = description;
            post.UpdateNum = updateNum;
            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return post;
        }

        /// <summary>
        /// Returns a count of all the posts in the database
        /// </summary>
        /// <returns>Count of all posts</returns>
        public async Task<int> GetCount()
        {
            return await _context.Post.CountAsync();
        }

        /// <summary>
        /// Determines if there is a next page
        /// </summary>
        /// <param name="page">The page number to get</param>
        /// <returns>A boolean</returns>
        public async Task<bool> CanPageRight(int page)
        {
            int total = await _context.Post.CountAsync();
            int skip = 5 * (page - 1);
            return skip < total;
        }

        /// <summary>
        /// Returns one page worth of posts (5 max)
        /// </summary>
        /// <param name="page">The page number to get</param>
        /// <returns>5 posts</returns>
        public async Task<List<Post>> GetPage(int page)
        {
            int skip = 5 * (page - 1);

            return await _context.Post.Include(x => x.Comments)
            .OrderByDescending(x => x.Date)
            .Skip(skip)
            .Take(5)
            .ToListAsync();
        }

        /// <summary>
        /// Returns the last page of posts
        /// </summary>
        /// <returns>Int of last page</returns>
        public async Task<int> GetLastPage()
        {
            decimal postCount = await _context.Post.CountAsync();
            var lastPage = Math.Ceiling(decimal.Divide(postCount, 5));
            return Decimal.ToInt32(lastPage);
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