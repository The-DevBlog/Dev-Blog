using Dev_Blog.Data;
using Dev_Blog.Interfaces;
using Dev_Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Services
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration _config;
        private readonly UserManager<UserModel> userMgr;
        private readonly UserDbContext _userDb;

        public EmailRepository(UserDbContext userdb, IConfiguration config, UserManager<UserModel> um)
        {
            _userDb = userdb;
            userMgr = um;
            _config = config;
        }

        /// <summary>
        /// Emails a welcome message to a newly registered user
        /// </summary>
        /// <param name="email">User's email</param>
        /// <returns>Successful completion of task</returns>
        public async Task Welcome(string email)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                TemplateId = Environment.GetEnvironmentVariable("SENDGRID_WELCOME_TEMPLATE"),
                From = new EmailAddress(Environment.GetEnvironmentVariable("ADMIN_EMAIL"))
            };

            msg.AddTo(email);

            await client.SendEmailAsync(msg);
        }

        /// <summary>
        /// Sends an email to all subscribed users when a new post is made
        /// </summary>
        /// <param name="img">The image to attach</param>"
        /// <returns>Successful completion of task</returns>
        public async Task NewPost(string img = null)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                TemplateId = Environment.GetEnvironmentVariable("SENDGRID_NEW_POST_TEMPLATE"),
                From = new EmailAddress(Environment.GetEnvironmentVariable("ADMIN_EMAIL"))
            };

            // get all users who are subscribed
            string username = Environment.GetEnvironmentVariable("ADMIN_USERNAME");
            List<UserModel> users = userMgr.Users.Where(x => x.UserName != username && x.Subscribed).ToList();

            if (users.Count > 0)
            {
                List<EmailAddress> emails = new List<EmailAddress>();
                users.ForEach(x => emails.Add(new EmailAddress(x.Email)));
                msg.AddTos(emails);
                await client.SendEmailAsync(msg);
            }
        }

        /// <summary>
        /// Checks whether an email already exists
        /// </summary>
        /// <param name="email"></param>
        /// <returns>bool</returns>
        public bool CheckEmail(string email)
        {
            var emails = _userDb.Users.Select(x => x.NormalizedEmail).ToList();
            return emails.Contains(email.ToUpper()) ? true : false;
        }

        /// <summary>
        /// Checks whether a username already exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns>bool</returns>
        public bool CheckUsername(string username)
        {
            var users = _userDb.Users.Select(x => x.NormalizedUserName).ToList();
            return users.Contains(username.ToUpper()) ? true : false;
        }
    }
}