using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dev_Blog.Models;
using Dev_Blog.Models.Interfaces;
using Dev_Blog.Models.ViewModels;
using Dev_Blog.Pages.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dev_Blog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IPost _post;
        private readonly SignInManager<User> _signInManager;

        [BindProperty]
        public LoginVM Vm { get; set; }

        //[BindProperty]
        //public Post Post { get; set; }

        public IndexModel(SignInManager<User> signInManager, IPost post)
        {
            _post = post;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            Vm.LatestPost = await _post.GetLatestPost();

            //Post = await _post.GetLatestPost();
            return Page();
        }

        public async Task<IActionResult> OnPostLogin()
        {
            var result = await _signInManager.PasswordSignInAsync(Vm.UserName, Vm.Password, false, false);
            if (result.Succeeded)
                Response.Redirect(Request.Path.ToString());

            ModelState.AddModelError("", "Invalid email or password");

            return Page();
        }
    }
}