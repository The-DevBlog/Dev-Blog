using Dev_Blog.Data;
using Dev_Blog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}