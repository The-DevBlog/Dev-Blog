using Dev_Blog.Models;
using Dev_Blog.Models.Base;
using Microsoft.AspNetCore.Identity;

namespace Dev_Blog.Pages
{
    public class UnsubscribeModel : BasePage
    {
        public UnsubscribeModel(SignInManager<User> signInManager) : base(signInManager)
        {
        }

        public void OnGet()
        {
        }
    }
}