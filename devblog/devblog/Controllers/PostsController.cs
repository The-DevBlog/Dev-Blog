using devblog.Interfaces;
using devblog.Models;
using Microsoft.AspNetCore.Mvc;

namespace devblog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _posts;

        public PostsController(IPostService posts)
        {
            _posts = posts;
        }

        /// <summary>
        /// Retrieves all posts
        /// </summary>
        /// <returns>List<PostModel></returns>
        [HttpGet]
        public async Task<List<PostModel>> Get()
        {
            var posts = await _posts.Get();
            return posts;
        }

        /// <summary>
        /// Retrieves all posts
        /// </summary>
        /// <returns>List<PostModel></returns>
        [HttpGet("{id}")]
        public async Task<PostModel> Get(int id)
        {
            var posts = await _posts.Get(id);
            return posts;
        }

        /// <summary>
        /// Adds a new post
        /// </summary>
        /// <param name="post">New post model</param>
        /// <returns>PostModel</returns>
        [HttpPost]
        public async Task<PostModel> Create(PostModel post)
        {
            var newPost = await _posts.Create(post.Description, post.ImgURL, post.UpdateNum);
            return newPost;
        }

        /// <summary>
        /// Removes a specified post
        /// </summary>
        /// <param name="postId">Post Id</param>
        /// <returns>Successful completion of task</returns>
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _posts.Delete(id);
        }
    }
}

