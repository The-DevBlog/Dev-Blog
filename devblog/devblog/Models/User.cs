using Microsoft.AspNetCore.Identity;

namespace devblog.Models
{
    public class User : IdentityUser
    {
        public bool Subscribed { get; set; }
    }
}