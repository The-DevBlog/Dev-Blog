using BlazorServer.Interfaces;
using BlazorServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Services
{
    public class EmailService : IEmails
    {
        private readonly IConfiguration _config;
        private readonly UserManager<UserModel> userMgr;

        public EmailService(IConfiguration config, UserManager<UserModel> um)
        {
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
    }
}