using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlazorServer.Models
{
    public class UserModel : IdentityUser
    {
        public bool Subscribed { get; set; }
    }
}