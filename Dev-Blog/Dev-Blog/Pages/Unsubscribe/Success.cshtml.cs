using System.Threading.Tasks;
using Dev_Blog.Models;
using Dev_Blog.Models.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dev_Blog.Pages.Unsubscribe
{
    public class SuccessModel : BasePage
    {
        private readonly UserManager<User> _userManager;

        public SuccessModel(UserManager<User> userManager, SignInManager<User> signInManager) : base(signInManager, userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            var userName = User.Identity.Name;
            User user = await _userManager.FindByNameAsync(userName);
            user.Subscribed = false;
            await _userManager.UpdateAsync(user);

            return Page();
        }
    }
}