using Dev_Blog.Models.Interfaces;
using Dev_Blog.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Models.Base
{
    public abstract class BasePage : PageModel
    {
        private readonly SignInManager<User> _signInManager;

        [BindProperty]
        public LoginVM LoginModelTest { get; set; }

        public BasePage(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostLogin()
        {
            var result = await _signInManager.PasswordSignInAsync(LoginModelTest.UserName, LoginModelTest.Password, false, false);
            if (result.Succeeded)
                Response.Redirect(Request.Path.ToString());

            ModelState.AddModelError("", "Invalid email or password");

            return Page();
        }
    }
}