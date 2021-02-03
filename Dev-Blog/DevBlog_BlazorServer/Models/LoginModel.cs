using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevBlog_BlazorServer.Models
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