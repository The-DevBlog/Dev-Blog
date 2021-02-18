using BlazorServer.Data;
using BlazorServer.Interfaces;
using BlazorServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Services
{
    public class PostRepository : IPostRepository
    {
        //TODO: Summary comments
        //TODO: change all Interfaces to 'IPostService'
        //TODO: change PostService to 'PostRepository'

        private AppDbContext _db;

        public PostRepository(AppDbContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Adds a new post to the database
        /// </summary>
        /// <param name="post">The new post</param>
        /// <param name="url">The url of the image</param>
        /// <returns>New post</returns>
        public async Task<PostModel> Create(PostModel post, string url)
        {
            post.ImgURL = url;

            //TODO: change to datetimeoffset.utcnow
            //post.Date = DateTimeOffset.UtcNow;

            post.Date = DateTime.Now;

            var newPost = _db.Post.Add(post).Entity;
            await _db.SaveChangesAsync();

            return newPost;
        }

        public async Task<List<PostModel>> GetPosts()
        {
            var posts = await _db.Post.OrderByDescending(x => x.Date)
                                      .Include(x => x.Comments)
                                      .Include(x => x.UpVotes)
                                      .Include(x => x.DownVotes)
                                      .ToListAsync();
            return posts;
        }

        /// <summary>
        /// Retrieves a specified post
        /// </summary>
        /// <param name="postId">Id of specified post</param>
        /// <returns>Specified post</returns>
        public async Task<PostModel> GetPost(int postId)
        {
            var post = await _db.Post.Include(x => x.Comments)
                                     .Include(x => x.UpVotes)
                                     .Include(x => x.DownVotes)
                                     .Where(p => p.Id == postId)
                                     .FirstOrDefaultAsync();

            return post;
        }

        public async Task UpdatePost(PostModel post)
        {
            _db.Post.Update(post);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a specified post from the database
        /// </summary>
        /// <param name="postId">Id of post to delete</param>
        /// <returns>Successful completion of task</returns>
        public async Task Delete(int postId)
        {
            var post = await GetPost(postId);
            _db.Remove(post);
            await _db.SaveChangesAsync();
        }
    }
}