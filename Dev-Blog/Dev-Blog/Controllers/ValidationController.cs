using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Dev_Blog.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dev_Blog.Controllers
{
    [ApiController]
    public class ValidationController : ControllerBase
    {
        private readonly UserDbContext _userContext;

        public ValidationController(UserDbContext userContext)
        {
            _userContext = userContext;
        }

        [HttpPost("/checkUserName")]
        public async Task<string> UserNameExists([FromBody] string username)
        {
            bool result = _userContext.Users.Any(x => x.NormalizedUserName == username.ToUpper());
            return JsonConvert.SerializeObject(result);
        }
    }
}