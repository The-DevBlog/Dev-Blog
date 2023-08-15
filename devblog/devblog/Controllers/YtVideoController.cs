using devblog.Interfaces;
using devblog.Models;
using Microsoft.AspNetCore.Mvc;

namespace devblog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YtVideoController : ControllerBase
    {
        private readonly IYtVideoService _video;

        public YtVideoController(IYtVideoService video)
        {
            _video = video;
        }

        /// <summary>
        /// Gets the URL for the homepage YouTube video
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<YtVideo> Get()
        {
            var video = await _video.GetVideo();
            return video;
        }
    }
}
