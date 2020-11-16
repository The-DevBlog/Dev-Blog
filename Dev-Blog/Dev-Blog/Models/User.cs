using Microsoft.AspNetCore.Identity;

namespace Dev_Blog.Models
{
    public class User : IdentityUser
    {
        public bool Subscribed { get; set; }
    }
}