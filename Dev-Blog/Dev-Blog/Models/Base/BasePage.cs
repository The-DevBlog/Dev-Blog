using Dev_Blog.Models.Interfaces;
using Dev_Blog.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dev_Blog.Models.Base
{
    public abstract class BasePage : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        [BindProperty]
        public AccountVM Input { get; set; }

        public BasePage(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public BasePage(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnPostLogin()
        {
            var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, false, false);
            if (result.Succeeded)
                Response.Redirect(Request.Path.ToString());

            ModelState.AddModelError("", "Invalid email or password");

            return Page();
        }

        public async Task<IActionResult> OnPostRegister()
        {
            User user = new User()
            {
                UserName = Input.UserName,
                Email = Input.Email
            };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                Claim userName = new Claim("UserName", Input.UserName);
                await _userManager.AddClaimAsync(user, userName);

                await _signInManager.SignInAsync(user, isPersistent: false);
                Response.Redirect(Request.Path.ToString());
            }
            return Page();
        }
    }
}