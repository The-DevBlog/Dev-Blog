using Dev_Blog.Models;
using Dev_Blog.Models.Base;
using Dev_Blog.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Dev_Blog.Pages.Account
{
    public class LoginErrorModel : BasePage
    {
        public LoginErrorModel(IEmail email, SignInManager<User> signInManager, UserManager<User> userManager) : base(signInManager, userManager, email)
        {
        }
    }
}