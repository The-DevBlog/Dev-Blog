namespace devblog.Interfaces
{
    public interface IEmailService
    {
        /// <summary>
        /// Emails a welcome message to a newly registered user
        /// </summary>
        Task Welcome(string email);

        /// <summary>
        /// Sends an email whenever a new post is made
        /// </summary>
        Task NewPost();
    }
}
