using devblog.Data;
using devblog.Interfaces;
using devblog.Models;
using Microsoft.EntityFrameworkCore;
using Discord;
using Discord.WebSocket;
using Mastonet;
using System.Net;

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

            // set up discord client
            _discordClient = discordClient;
            _discordClient.LoginAsync(TokenType.Bot, _config.GetValue<string>("DiscordToken"));
        }

        /// <summary>
        /// Creates a new post
        /// </summary>
        /// <param name="description">Description of post</param>
        /// <param name="files">Files to upload</param>
        /// <returns>UploadStatus</returns>
        public async Task<UploadStatus> Create(string description, IFormFile[] files)
        {
            var newPost = new Post()
            {
                Date = DateTime.Now,
                Description = description,
            };

            var res = _db.Post.Add(newPost).Entity;

            var uploadStatus = new UploadStatus
            {
                DiscordStatus = await PostToDiscord(description, files),
                MastodonStatus = await PostToMastodon(description, files)
            };

            // only create post on the devblog if posts are successful on other clients
            if(uploadStatus.DiscordStatus.IsSuccessStatusCode && uploadStatus.MastodonStatus.IsSuccessStatusCode)
            {
                await _db.SaveChangesAsync();
                await _imgService.Create(files, res.Id);
            }

            return uploadStatus;
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

            return post;
        }

        /// <summary>
        /// Updates a post
        /// </summary>
        /// <param name="id">Id of post to be updated</param>
        /// <param name="description">New description of post</param>
        /// <returns>Successful completion of task</returns>
        public async Task<Post> Update(int id, string description)
        {
            var post = await _db.Post.Where(p => p.Id == id).FirstOrDefaultAsync();
            post.Description = description;
            _db.Post.Update(post);
            await _db.SaveChangesAsync();

            return post;
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
        private async Task<HttpResponseMessage> PostToDiscord(string description, IFormFile[] files)
        {
            await _discordClient.StartAsync().WaitAsync(TimeSpan.FromSeconds(15));

            var channel = await _discordClient.GetChannelAsync(_config.GetValue<ulong>("DiscordChannelId")) as IMessageChannel;
            var res = new HttpResponseMessage();

            // add imgs to request
            List<FileAttachment> attachments = new List<FileAttachment>();
            foreach (var file in files)
                attachments.Add(new FileAttachment(file.OpenReadStream(), file.FileName));

            try
            {
                await channel.SendFilesAsync(attachments, description);
                await _discordClient.StopAsync();
            }
            catch (Exception e)
            {
                res.StatusCode = HttpStatusCode.BadRequest;
                res.ReasonPhrase = "Failed to post to  Discord: " + e.Message;
                return res;
            }

            res.StatusCode = HttpStatusCode.OK;
            return res;
        }

        /// <summary>
        /// Sends new post to Mastodon
        /// </summary>
        /// <param name="description">Description of new post</param>
        /// <param name="files">Images of new post</param>
        private async Task<HttpResponseMessage> PostToMastodon(string description, IFormFile[] files)
        {
            var client = new MastodonClient("mastodon.social", _config.GetValue<string>("MastodonToken"));
            var res = new HttpResponseMessage();

            // add imgs to request
            List<string> attachments = new List<string>();

            try
            {
                foreach (var file in files)
                {
                    var media = new MediaDefinition(file.OpenReadStream(), file.FileName);
                    var mediaId = await client.UploadMedia(media);
                    attachments.Add(mediaId.Id);
                }
                await client.PublishStatus(description, mediaIds: attachments);
            }
            catch (Exception e)
            {
                res.StatusCode = HttpStatusCode.BadRequest;
                res.ReasonPhrase = "Failed to post to  Mastodon: " + e.Message;
                return res;
            }

            res.StatusCode = HttpStatusCode.OK;
            return res;
        }
    }
}