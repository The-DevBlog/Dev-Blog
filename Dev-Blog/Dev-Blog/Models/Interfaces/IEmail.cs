using Dev_Blog.Models.ViewModels;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <returns>Successful completion of task</returns>
        Task NewPost();

        /// <summary>
        /// Sends a suggestion to the admin
        /// </summary>
        /// <param name="email">The admin's email</param>
        /// <param name="context">Context of email</param>
        /// <returns>Successful completion of task</returns>
        public Task Suggestion(string email, string context);
    }
}