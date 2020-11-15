using Dev_Blog.Data;
using Dev_Blog.Models.Interfaces;
using Dev_Blog.Models.ViewModels;
using DnsClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Dev_Blog.Models.Services
{
    public class EmailService : IEmail
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;

        public EmailService(IConfiguration config, UserManager<User> userManager)
        {
            _userManager = userManager;
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
                TemplateId = _config.GetSection("WELCOME_EMAIL").Value,
                From = new EmailAddress(_config.GetSection("AdminEmail").Value),
            };

            msg.AddTo(email);

            await client.SendEmailAsync(msg);
        }

        /// <summary>
        /// Sends an email to all subscribed users when a new post is made
        /// </summary>
        /// <returns>Successful completion of task</returns>
        public async Task NewPost()
        {
            var apiKey = _config.GetSection("SENDGRID_APIKEY").Value;
            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_config.GetSection("AdminEmail").Value),
                Subject = "New Post",
                HtmlContent = "<p>Check out this new post!</p>"
            };

            // get all users who are subscribed
            List<User> users = _userManager.Users.Where(x => x.UserName != _config.GetSection("AdminUserName").Value && x.Subscribed).ToList();

            if (users.Count > 0)
            {
                List<EmailAddress> emails = new List<EmailAddress>();
                users.ForEach(x => emails.Add(new EmailAddress(x.Email)));
                msg.AddTos(emails);
                await client.SendEmailAsync(msg);
            }
        }

        // TODO: not working
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