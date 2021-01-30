using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevBlog_BlazorServer.Models
{
    public class UserModel : IdentityUser
    {
        public bool Subscribed { get; set; }
    }
}