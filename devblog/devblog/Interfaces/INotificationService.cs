namespace devblog.Interfaces
{
    public interface INotificationService
    {
        /// <summary>
        /// Creates a noticication for a new post to every user
        /// </summary>
        Task Create(int PostId);
    }
}
