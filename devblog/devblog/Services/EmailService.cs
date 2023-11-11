using SendGrid.Helpers.Mail;
using SendGrid;
using devblog.Data;
using Microsoft.AspNetCore.Identity;
using devblog.Interfaces;

namespace devblog.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        //private readonly UserManager<UserModel> userMgr;
        //private readonly UserDbContext _userDb;
        private readonly EmailAddress _email = new EmailAddress();

        public EmailService(UserDbContext userdb, IConfiguration config)
        {
            //_userDb = userdb;
            //userMgr = um;
            _config = config;
        }

        /// <summary>
        /// Emails a welcome message to a newly registered user
        /// </summary>
        /// <param name="email">User's email</param>
        /// <returns>Successful completion of task</returns>
        public async Task Welcome(string email)
        {
            _email.Email = _config["emailAddress"];
            var apiKey = _config["SendGridApiKey"];
            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                TemplateId = _config["SendGridWelcomeTemplate"],
                From = _email,
            };

            msg.AddTo(email);

            var t = await client.SendEmailAsync(msg);
            await Console.Out.WriteLineAsync("HELO");
        }
    }
}
