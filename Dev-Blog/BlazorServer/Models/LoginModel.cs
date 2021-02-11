using System.ComponentModel.DataAnnotations;

namespace BlazorServer.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(25)]
        public string UserName { get; set; }

        //[Required]
        //public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}