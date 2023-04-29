using devblog.Interfaces;
using devblog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace devblog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _posts;
        private readonly IImgService _imgService;

        public PostsController(IPostService posts, IImgService imgService)
        {
            _posts = posts;
            _imgService = imgService;
        }

        /// <summary>
        /// Retrieves all posts
        /// </summary>
        /// <returns>List<Post></returns>
        [HttpGet]
        public async Task<List<Post>> Get()
        {
            var posts = await _posts.Get();
            return posts;
        }

        /// <summary>
        /// Retrieves all posts
        /// </summary>
        /// <returns>List<Post></returns>
        [HttpGet("{id}")]
        public async Task<Post> Get(int id)
        {
            var posts = await _posts.Get(id);
            return posts;
        }

        /// <summary>
        /// Adds a new post
        /// </summary>
        /// <param name="file">Data for new post</param>
        /// <returns>Post</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<Post> Create(IFormFile file)
        {
            var description = Request.Form["description"];
            var updateNum = Request.Form["updateNum"];

            var stream = file.OpenReadStream();
            string imgUrl = await _imgService.AddImgToDropBox(stream, file.FileName);
            var newPost = await _posts.Create(description, imgUrl, updateNum);

            return newPost;
        }

        /// <summary>
        /// Removes a specified post
        /// </summary>
        /// <param name="postId">Post Id</param>
        /// <returns>Successful completion of task</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _posts.Delete(id);
        }
    }

    public class Payload
    {
        public IFormFile file { get; set; }
    }

    public class ImageDTO
    {
        public string FileName { get; set; }

        public IFormFile Image { get; set; }
    }

    public class Image
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public byte[] Picture { get; set; }
        public List<User> Users { get; set; }
    }
}

