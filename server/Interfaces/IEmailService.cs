using devblog.Models;
using SendGrid;
using static devblog.Services.EmailService;

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

        /// <summary>
        /// Subscribes a user to the email list
        /// </summary>
        Task<Response> EmailSubscribe(string email);

        /// <summary>
        /// Toggles a specific users email preference
        /// </summary>
        Task<bool> ToggleSubscribe(User user);

        /// <summary>
        /// Checks to see if a user is subscribed to email
        /// </summary>
        Task<bool> IsSubscribed(string email);

        /// <summary>
        /// Retrieves all Contacts from a Specific Contact list. If env is prod, it will get contacts in 'TheDevBlog_prod' list. If env is staging or test, it will get 'TheDevBlog_staging' list.
        /// </summary>
        /// <returns>List<Contact></returns>
        /// <exception cref="Exception"></exception>
        Task<List<Contact>> GetContactsForList();
    }
}
