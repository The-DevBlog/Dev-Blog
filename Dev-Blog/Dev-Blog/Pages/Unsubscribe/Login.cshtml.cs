using System.Threading.Tasks;
using Dev_Blog.Models;
using Dev_Blog.Models.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dev_Blog.Pages.Unsubscribe
{
    public class LoginModel : BasePage
    {
        private readonly SignInManager<User> _signInManager;

        public LoginModel(SignInManager<User> signInManager) : base(signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToPage("/Unsubscribe/Success");
            }
            else return RedirectToPage("/Account/LoginError");
        }

        public void OnGet()
        {
        }
    }
}