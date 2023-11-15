using SendGrid.Helpers.Mail;
using SendGrid;
using devblog.Interfaces;
using Microsoft.AspNetCore.Identity;
using devblog.Models;
using Microsoft.EntityFrameworkCore;

namespace devblog.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public UserManager<User> _userMgr { get; }
        private readonly EmailAddress _email;
        private readonly SendGridClient _sendGridClient;

        public EmailService(IConfiguration config, UserManager<User> usermgr)
        {
            _userMgr = usermgr;
            _config = config;
            _email = new EmailAddress(_config["emailAddress"]);
            _sendGridClient = new SendGridClient(_config["SendGridApiKey"]);
        }

        /// <summary>
        /// Sends an email to all subscribed users whenever a new post is made
        /// </summary>
        public async Task NewPost()
        {
            var allUsers = await _userMgr.Users.ToListAsync();

            allUsers.ForEach(async user =>
            {
                if (user.Subscribed)
                {
                    var toEmail = new EmailAddress(user.Email);

                    var msg = MailHelper.CreateSingleTemplateEmail(_email, toEmail, _config["SendGridNewPostTemplateId"], null);
                    var res = await _sendGridClient.SendEmailAsync(msg);
                }
            });
        }

        /// <summary>
        /// Emails a welcome message to a newly registered user
        /// </summary>
        public async Task Welcome(string email)
        {
            var toEmail = new EmailAddress(email);

            var msg = MailHelper.CreateSingleTemplateEmail(_email, toEmail, _config["SendGridWelcomeTemplateId"], null);
            var res = await _sendGridClient.SendEmailAsync(msg);
        }
    }
}
