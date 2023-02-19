using devblog.Interfaces;
using devblog.Models;
using Microsoft.AspNetCore.Mvc;

namespace devblog.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        [HttpGet("get")]
        public async Task<List<PostModel>> Get()
        {
            var posts = await _posts.Get();
            return posts;
        }
    }
}

