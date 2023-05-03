﻿using devblog.Data;
using devblog.Interfaces;
using devblog.Models;
using Microsoft.EntityFrameworkCore;
using Discord;
using Discord.WebSocket;

namespace devblog.Services
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _db;
        private readonly IImgService _imgService;
        private readonly IConfiguration _config;
        private readonly DiscordSocketClient _discordClient;

        public PostService(AppDbContext context, IImgService imgService, DiscordSocketClient discordClient, IConfiguration config)
        {
            _db = context;
            _imgService = imgService;
            _config = config;
            _discordClient = discordClient;

            _discordClient.LoginAsync(TokenType.Bot, _config.GetValue<string>("DiscordToken"));
        }

        /// <summary>
        /// Creates a new post
        /// </summary>
        /// <param name="description">Description of post</param>
        /// <param name="imgURLs">Img URLs of post</param>
        /// <param name="updateNum">Update number of post</param>
        /// <param name="files">Files to upload</param>
        /// <returns>Post</returns>
        public async Task<Post> Create(string description, string updateNum, IFormFile[] files)
        {
            var newPost = new Post()
            {
                Date = DateTime.Now,
                Description = description,
                UpdateNum = updateNum
            };

            var res = _db.Post.Add(newPost).Entity;
            await _db.SaveChangesAsync();
            await _imgService.Create(files, res.Id);

            await PostToDiscord(description, files);
            await PostToReddit();

            return res;
        }


        /// <summary>
        /// Retrieves all posts
        /// </summary>
        /// <returns>List<Post></returns>
        public async Task<List<Post>> Get()
        {
            var posts = await _db.Post.OrderByDescending(x => x.Date)
                                      .Include(x => x.Comments)
                                      .Include(x => x.Imgs)
                                      .Include(x => x.UpVotes)
                                      .Include(x => x.DownVotes)
                                      .ToListAsync();

            return posts;
        }

        /// <summary>
        /// Retrieves a specified post
        /// </summary>
        /// <param name="postId">Post Id</param>
        /// <returns>Post</returns>
        public async Task<Post> Get(int postId)
        {
            Post post;

            // if id is -1, get latest post, else get specified post
            if (postId == -1)
            {
                post = await _db.Post.OrderByDescending(x => x.Date)
                                         .Include(x => x.Comments)
                                         .Include(x => x.Imgs)
                                         .Include(x => x.UpVotes)
                                         .Include(x => x.DownVotes)
                                         .FirstOrDefaultAsync();
            }
            else
            {
                post = await _db.Post.Include(x => x.Comments)
                                         .Include(x => x.Imgs)
                                         .Include(x => x.UpVotes)
                                         .Include(x => x.DownVotes)
                                         .Where(p => p.Id == postId)
                                         .FirstOrDefaultAsync();
            }

            post.Imgs.Reverse();
            return post;
        }

        /// <summary>
        /// Updates a post
        /// </summary>
        /// <param name="post"></param>
        /// <returns>Successful completion of task</returns>
        public async Task Update(Post post)
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

        /// <summary>
        /// Sends new post to discord
        /// </summary>
        /// <param name="description">Description of new post</param>
        /// <param name="files">Images of new post</param>
        private async Task PostToDiscord(string description, IFormFile[] files)
        {
            await _discordClient.StartAsync().WaitAsync(TimeSpan.FromSeconds(15));

            var channel = await _discordClient.GetChannelAsync(_config.GetValue<ulong>("DiscordChannelId")) as IMessageChannel;

            List<FileAttachment> filesToSend = new List<FileAttachment>();

            foreach (var file in files)
            {
                var stream = file.OpenReadStream();
                filesToSend.Add(new FileAttachment(stream, file.FileName));
            }

            await channel.SendFilesAsync(filesToSend, description);
            await _discordClient.StopAsync();
        }

        /// <summary>
        /// Sends new post to Reddit
        /// </summary>
        /// <param name="description">Description of new post</param>
        /// <param name="files">Images of new post</param>
        private async Task PostToReddit()
        {

        }
    }
}