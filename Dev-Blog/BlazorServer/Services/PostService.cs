﻿using BlazorServer.Data;
using BlazorServer.Interfaces;
using BlazorServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Services
{
    public class PostService : IPosts
    {
        private AppDbContext _db;

        public PostService(AppDbContext context)
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
            post.Date = DateTime.Now;

            _db.Entry(post).State = EntityState.Added;
            await _db.SaveChangesAsync();

            return post;
        }

        public async Task<List<PostModel>> GetPosts()
        {
            var posts = await _db.Post.ToListAsync();
            return posts;
        }
    }
}