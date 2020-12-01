using System;
using System.Threading.Tasks;
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
            string email, username;

            if (input.UserName == "")
                username = null;
            else
                username = _validator.UserNameExists(input.UserName).ToString();

            if (input.Email == "")
                email = null;
            else
                email = _validator.EmailExists(input.Email).ToString();

            Object[] json = { email, username };
            return JsonConvert.SerializeObject(json);
        }
    }
}