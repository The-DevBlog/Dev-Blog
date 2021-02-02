using Identity.Dapper.Entities;
using System.ComponentModel.DataAnnotations;

namespace DevBlog_BlazorServer.Models
{
    public class UserModel : DapperIdentityUser
    {
        public bool Subscribed { get; set; }

        //[Required]
        //[EmailAddress]
        //[RegularExpression("^(?=.{1,64}@)[A-Za-z0-9_-]+(\\.[A-Za-z0-9_-]+)*@[^-][A-Za-z0-9-]+(\\.[A-Za-z0-9-]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "Please enter a correct email address")]
        //public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}