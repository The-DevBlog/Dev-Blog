using Microsoft.AspNetCore.Identity;

namespace DevBlog_BlazorServer.Models
{
    public class UserModel : IdentityUser
    {
        public bool Subscribed { get; set; }
    }
}