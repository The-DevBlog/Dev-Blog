using SendGrid.Helpers.Mail;
using SendGrid;
using devblog.Interfaces;
using Microsoft.AspNetCore.Identity;
using devblog.Models;
using Microsoft.EntityFrameworkCore;
using devblog.Data;
using Newtonsoft.Json.Linq;

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
        /// Subscribes a user to the email list
        /// </summary>
        public async Task<Response> EmailSubscribe(string email)
        {
            var data = $@"{{
                    ""list_ids"": [""{_config["SendGridDevBlogContactList"]}""],
                    ""contacts"": [
                        {{
                            ""email"": ""{email}""
                        }}
                    ]
                }}";

            var response = await _sendGridClient.RequestAsync(
                method: SendGridClient.Method.PUT,
                urlPath: "marketing/contacts",
                requestBody: data
            );

            Console.WriteLine();
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine();

            return response;
        }

        /// <summary>
        /// Toggles a specific users email preference
        /// </summary>
        public async Task<bool> ToggleSubscribe(User user)
        {
            var u = await _userDb.Users.Where(u => u == user).FirstOrDefaultAsync();
            u.Subscribed = !user.Subscribed;
            await _userDb.SaveChangesAsync();

            Response response = null;

            // add or remove the email from the send grid contact list
            if (!u.Subscribed)
            {
                // retrive the contact
                var data = $@"{{
                    ""emails"": [""{user.Email}""]
                }}";

                var contact = await _sendGridClient.RequestAsync(
                    method: SendGridClient.Method.POST,
                    urlPath: "marketing/contacts/search/emails",
                    requestBody: data
                );

                // extract the id from the contact
                JObject json = JObject.Parse(await contact.Body.ReadAsStringAsync());
                string id = json["result"][user.Email]["contact"]["id"].ToString();

                // delete the contact from the list
                var queryParams = $@"{{
                    ""ids"": [""{id}""]
                }}";

                response = await _sendGridClient.RequestAsync(
                    method: SendGridClient.Method.DELETE,
                    urlPath: "marketing/contacts",
                    queryParams: queryParams
                );
            }
            else
                response = EmailSubscribe(u.Email).Result;

            Console.WriteLine();
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine();

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
