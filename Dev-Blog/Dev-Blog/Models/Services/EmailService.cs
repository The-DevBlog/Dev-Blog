using Dev_Blog.Models.Interfaces;
using Dev_Blog.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Models.Services
{
    public class EmailService : IEmail
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Emails a welcome message to a newly registered user
        /// </summary>
        /// <param name="email">User's email</param>
        /// <returns>Successful completion of task</returns>
        public async Task Welcome(string email)
        {
            var apiKey = _config.GetSection("SENDGRID_APIKEY").Value;
            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_config.GetSection("AdminEmail").Value),
                Subject = "Welcome",
                HtmlContent = "<p>Thank you for subscribing!</p>"
            };

            msg.AddTo(email);
            await client.SendEmailAsync(msg);
        }

        /// <summary>
        /// Sends a suggestion to the admin
        /// </summary>
        /// <param name="email">The admin's email</param>
        /// <param name="context">Context of email</param>
        /// <returns>Successful completion of task</returns>
        public async Task Suggestion(string email, string context)
        {
            var apiKey = _config.GetSection("SENDGRID_APIKEY").Value;
            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(email),
                Subject = "Suggestion",
                HtmlContent = $"<p>{context}</p>"
            };
            msg.AddTo(_config.GetSection("AdminEmail").Value);
            await client.SendEmailAsync(msg);
        }
    }
}