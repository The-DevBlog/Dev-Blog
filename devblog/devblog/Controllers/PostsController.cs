﻿using devblog.Interfaces;
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
        //[Authorize(Roles = "Admin")]
        //[HttpPost]
        //public async Task<Post> Create(IFormFile[] files)
        //{
        //    var description = Request.Form["description"];
        //    var updateNum = Request.Form["updateNum"];

        //    List<string> imgUrls = new List<string>();
        //    foreach (var f in files)
        //    {
        //        var stream = f.OpenReadStream();
        //        string imgUrl = await _imgService.AddImgToDropBox(stream, f.FileName);
        //        imgUrls.Add(imgUrl);
        //    }

        //    var newPost = await _posts.Create(description, imgUrl, updateNum);
        //    return newPost;
        //}

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<Post> Create(IFormFile[] files)
        {
            var description = Request.Form["description"];
            var updateNum = Request.Form["updateNum"];

            //List<string> imgUrls = new List<string>();
            //foreach (var f in files)
            //{
            //    var stream = f.OpenReadStream();
            //    string imgUrl = await _imgService.AddImgToDropBox(stream, f.FileName);
            //    imgUrls.Add(imgUrl);
            //}

            var newPost = await _posts.Create(description, updateNum, files);
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
}

