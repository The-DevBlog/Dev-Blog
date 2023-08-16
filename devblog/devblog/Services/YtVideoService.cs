using devblog.Data;
using devblog.Interfaces;
using devblog.Models;
using Microsoft.EntityFrameworkCore;

namespace devblog.Services
{
    public class YtVideoService : IYtVideoService
    {
        private readonly AppDbContext _db;

        public YtVideoService(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Gets the URL for the homepage YouTube video
        /// </summary>
        /// <returns></returns>
        public async Task<YtVideo> Get()
        {
            var video = await _db.YtVideo.FirstAsync();
            return video;
        }

        /// <summary>
        /// Updates the url of the homepage YouTube video
        /// </summary>
        /// <param name="url">new url for video</param>
        /// <returns>Updated Video</returns>
        public async Task<YtVideo> Update(string url)
        {
            var video = await _db.YtVideo.FirstAsync();
            video.Url = url;
            _db.YtVideo.Update(video);
            await _db.SaveChangesAsync();
            return video;
        }
    }
}
