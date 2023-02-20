using devblog.Data;
using devblog.Interfaces;
using devblog.Models;
using Microsoft.EntityFrameworkCore;

namespace devblog.Services
{
    public class PostService : IPostService
    {
        private AppDbContext _db;

        public PostService(AppDbContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Adds a new post
        /// </summary>
        /// <param name="post"></param>
        /// <param name="url">The url of the post's image</param>
        /// <returns>PostModel</returns>
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

        /// <summary>
        /// Retrieves all posts
        /// </summary>
        /// <returns>List<PostModel></returns>
        public async Task<List<PostModel>> Get()
        {
            var posts = await _db.Post.OrderByDescending(x => x.Date)
                                      //.Include(x => x.Comments)
                                      //.Include(x => x.UpVotes)
                                      //.Include(x => x.DownVotes)
                                      .ToListAsync();

            return posts;
        }

        /// <summary>
        /// Retrieves a specified post
        /// </summary>
        /// <param name="postId">Post Id</param>
        /// <returns>PostModel</returns>
        public async Task<PostModel> Get(int postId)
        {
            PostModel post;

            // if id is -1, get latest post, else get specified post
            if (postId == -1)
            {
                post = await _db.Post.OrderByDescending(x => x.Date)
                                         //.Include(x => x.Comments)
                                         //.Include(x => x.UpVotes)
                                         //.Include(x => x.DownVotes)
                                         .FirstOrDefaultAsync();
            }
            else
            {
                post = await _db.Post.Include(x => x.Comments)
                                         //.Include(x => x.UpVotes)
                                         //.Include(x => x.DownVotes)
                                         .Where(p => p.Id == postId)
                                         .FirstOrDefaultAsync();
            }

            return post;
        }

        /// <summary>
        /// Updates a post
        /// </summary>
        /// <param name="post"></param>
        /// <returns>Successful completion of task</returns>
        public async Task Update(PostModel post)
        {
            _db.Post.Update(post);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a specified post
        /// </summary>
        /// <param name="postId">Post Id</param>
        /// <returns>Successful completion of task</returns>
        public async Task Delete(int postId)
        {
            var post = await Get(postId);
            _db.Remove(post);
            await _db.SaveChangesAsync();
        }
    }
}