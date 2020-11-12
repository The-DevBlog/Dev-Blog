using Dev_Blog.Data;
using Dev_Blog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Dev_Blog.Models.Services
{
    public class ValidatorService : IValidator
    {
        private readonly UserDbContext _userContext;

        public ValidatorService(UserDbContext userContext)
        {
            _userContext = userContext;
        }

        public bool UserNameExists(string userName)
        {
            return _userContext.Users.Any(x => x.NormalizedUserName == userName.ToUpper());
        }

        public bool EmailExists(string email)
        {
            return _userContext.Users.Any(x => x.NormalizedEmail == email.ToUpper());
        }

        // prevent CSS attacks
        public string ValidateComment(string comment)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(HttpUtility.HtmlEncode(comment));
            comment = sb.ToString();
            return HttpUtility.HtmlEncode(comment);
        }
    }
}