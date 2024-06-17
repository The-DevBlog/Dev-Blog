using SendGrid.Helpers.Mail;
using SendGrid;
using devblog.Interfaces;
using Microsoft.AspNetCore.Identity;
using devblog.Models;
using Microsoft.EntityFrameworkCore;
using devblog.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using static Dropbox.Api.Files.ListRevisionsMode;
using System.Text;
using Discord;

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
        /// Sends an email to all contacts in specific contact list whenever a new post is made
        /// </summary>
        public async Task NewPost()
        {
            var contacts = await GetContactsForList();
            contacts.ForEach(async contact =>
            {
                var toEmail = new EmailAddress(contact.email);
                var msg = MailHelper.CreateSingleTemplateEmail(_email, toEmail, _config["SendGridNewPostTemplateId"], null);
                var res = await _sendGridClient.SendEmailAsync(msg);
            });
        }

        /// <summary>
        /// Retrieves all Contacts from a Specific Contact list
        /// </summary>
        /// <returns>List<Contact></returns>
        /// <exception cref="Exception"></exception>
        private async Task<List<Contact>> GetContactsForList()
        {
            // export list
            var listId = _config["SendGridDevBlogContactList"];
            var data = $@"{{
                ""ids"": ""[{listId}]"", 
                ""file_type"": ""json""
            }}";

            var export_request = await _sendGridClient.RequestAsync(
                method: SendGridClient.Method.POST,
                urlPath: "marketing/contacts/exports",
                requestBody: data
            );
            string exportId = JsonConvert.DeserializeObject<Export>(export_request.Body.ReadAsStringAsync().Result).id;


            Export export = new Export();
            export.urls = new List<string>();
            int maxAttemps = 10;
            int i = 0;
            while (export.urls.Count == 0 && i++ < maxAttemps)
            {
                var export_url_request = await _sendGridClient.RequestAsync(
                    method: SendGridClient.Method.GET,
                    urlPath: $"marketing/contacts/exports/{exportId}"
                );
                export = JsonConvert.DeserializeObject<Export>(export_url_request.Body.ReadAsStringAsync().Result);

                if (export.urls.Count > 0)
                    break;
                else
                    await Task.Delay(1000);
            }

            if (export.urls.Count == 0)
                throw new Exception("Failed to retrieve export URL after multiple attempts.");

            // Download exported data
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(export.urls[0]);

            // Ensure the response indicates success
            response.EnsureSuccessStatusCode();

            // Read the response content as a stream and decompress if necessary
            using var stream = await response.Content.ReadAsStreamAsync();
            using var decompressedStream = new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress);
            using var reader = new StreamReader(decompressedStream, Encoding.UTF8);

            var contacts = new List<Contact>();
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                var contact = JsonConvert.DeserializeObject<Contact>(line);
                contacts.Add(contact);
            }

            return contacts;
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

        private class Export
        {
            public string id { get; set; }
            public List<string> urls { get; set; }
        }

        private class Contact
        {
            public string email { get; set; }
            public string contact_id { get; set; }
        }
    }
}
