using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Dev_Blog.Models
{
    public class UserModel : IdentityUser
{
    public bool Subscribed { get; set; }
}
}