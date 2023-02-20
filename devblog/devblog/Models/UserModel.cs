using Microsoft.AspNetCore.Identity;

namespace devblog.Models
{
    public class UserModel : IdentityUser
    {
        public bool Subscribed { get; set; }
    }
}