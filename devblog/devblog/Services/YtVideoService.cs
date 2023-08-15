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
        public async Task<YtVideo> GetVideo()
        {
            var video = await _db.YtVideo.FirstAsync();
            return video;
        }
    }
}
