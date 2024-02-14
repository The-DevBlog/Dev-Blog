using devblog.Models;

namespace devblog.Interfaces
{
    public interface INotificationService
    {
        /// <summary>
        /// Creates a noticication for a new post to every user
        /// </summary>
        Task Create(int PostId);

        /// <summary>
        /// Gets all notifications for a specific user
        /// </summary>
        /// <param name="userName">Username to retrive unseen notifications for</param>
        /// <returns>List<Notification></returns>
        Task<List<Notification>> Get(string userName);

        /// <summary>
        /// Delete a specified notification
        /// </summary>
        Task Delete(int postId, string userName);

        /// <summary>
        /// Delete all notifications for a specified post
        /// </summary>
        /// <param name="postId"></param>
        Task DeleteAllForPost(int postId);
    }
}
