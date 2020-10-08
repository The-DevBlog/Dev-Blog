using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev_Blog.Models;
using Dev_Blog.Models.Interfaces;
using Dev_Blog.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dev_Blog.Pages.Status
{
    public class IndexModel : PageModel
    {
        private readonly IPost _post;
        private readonly SignInManager<User> _signInManager;

        [BindProperty]
        public LoginVM Login { get; set; }

        [BindProperty]
        public List<Post> Posts { get; set; }

        public IndexModel(SignInManager<User> signInManager, IPost post)
        {
            _signInManager = signInManager;
            _post = post;
        }

        public async Task<IActionResult> OnGet()
        {
            Posts = await _post.GetAllPosts();
            return Page();
        }

        public async Task<IActionResult> OnPostLogin()
        {
            var result = await _signInManager.PasswordSignInAsync(Login.UserName, Login.Password, false, false);
            if (result.Succeeded)
                Response.Redirect(Request.Path.ToString());

            ModelState.AddModelError("", "Invalid email or password");

            return Page();
        }
    }
}