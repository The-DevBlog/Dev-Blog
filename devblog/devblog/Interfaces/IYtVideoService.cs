using devblog.Models;

namespace devblog.Interfaces
{
    public interface IYtVideoService
    {
        /// <summary>
        /// Gets the video for the homepage YouTube video
        /// </summary>
        /// <returns></returns>
        Task<YtVideo> Get();

        /// <summary>
        /// Updates the url of the homepage YouTube video
        /// </summary>
        /// <param name="url">new url for video</param>
        /// <returns>Updated Video</returns>
        Task<YtVideo> Update(string url);
    }
}
