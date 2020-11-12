using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Dev_Blog.Data;
using Dev_Blog.Models.Interfaces;
using Dev_Blog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dev_Blog.Controllers
{
    [ApiController]
    public class ValidatorController : ControllerBase
    {
        private readonly IValidator _validator;

        public ValidatorController(IValidator validator)
        {
            _validator = validator;
        }

        [HttpPost("/Validate")]
        public async Task<string> Validate([FromBody] ValidateVM input)
        {
            bool email = _validator.EmailExists(input.Email);
            bool userName = _validator.UserNameExists(input.UserName);

            Object[] json = { email, userName };

            return JsonConvert.SerializeObject(json);
        }
    }
}