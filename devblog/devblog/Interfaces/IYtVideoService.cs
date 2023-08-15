using devblog.Models;

namespace devblog.Interfaces
{
    public interface IYtVideoService
    {
        /// <summary>
        /// Gets the video for the homepage YouTube video
        /// </summary>
        /// <returns></returns>
        Task<YtVideo> GetVideo();
    }
}
