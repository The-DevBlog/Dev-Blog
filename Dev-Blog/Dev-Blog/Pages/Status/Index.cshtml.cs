using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev_Blog.Models;
using Dev_Blog.Models.Base;
using Dev_Blog.Models.Interfaces;
using Dev_Blog.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dev_Blog.Pages.Status
{
    public class IndexModel : BasePage
    {
        private readonly IPost _post;

        [BindProperty]
        public List<Post> Posts { get; set; }

        public IndexModel(SignInManager<User> signInManager, UserManager<User> userManager, IPost post) : base(signInManager, userManager)
        {
            _post = post;
        }

        public async Task<IActionResult> OnGet()
        {
            Posts = await _post.GetAllPosts();
            return Page();
        }
    }
}