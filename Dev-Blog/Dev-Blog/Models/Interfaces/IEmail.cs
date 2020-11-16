using System.Threading.Tasks;

namespace Dev_Blog.Models.Interfaces
{
    public interface IEmail
    {
        /// <summary>
        /// Emails a welcome message to a newly registered user
        /// </summary>
        /// <param name="email">User's email</param>
        /// <returns>Successful completion of task</returns>
        public Task Welcome(string email);

        /// <summary>
        /// Sends an email to all subscribed users when a new post is made
        /// </summary>
        /// <param name="img">The image to attach</param>"
        /// <returns>Successful completion of task</returns>
        Task NewPost(string img = null);

        /// <summary>
        /// Sends a suggestion to the admin
        /// </summary>
        /// <param name="email">The admin's email</param>
        /// <param name="context">Context of email</param>
        /// <returns>Successful completion of task</returns>
        public Task Suggestion(string email, string context);
    }
}