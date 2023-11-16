using SendGrid.Helpers.Mail;
using SendGrid;
using devblog.Interfaces;
using Microsoft.AspNetCore.Identity;
using devblog.Models;
using Microsoft.EntityFrameworkCore;
using devblog.Data;

namespace devblog.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly UserDbContext _userDb;
        public UserManager<User> _userMgr { get; }
        private readonly EmailAddress _email;
        private readonly SendGridClient _sendGridClient;

        public EmailService(IConfiguration config, UserManager<User> usermgr, UserDbContext userDb)
        {
            _userDb = userDb;
            _userMgr = usermgr;
            _config = config;
            _email = new EmailAddress(_config["emailAddress"]);
            _sendGridClient = new SendGridClient(_config["SendGridApiKey"]);
        }

        /// <summary>
        /// Toggles a specific users email preference
        /// </summary>
        public async Task<bool> ToggleSubscribe(User user)
        {
            var u = await _userDb.Users.Where(u => u == user).FirstOrDefaultAsync();
            u.Subscribed = !user.Subscribed;
            await _userDb.SaveChangesAsync();
            return u.Subscribed;
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
