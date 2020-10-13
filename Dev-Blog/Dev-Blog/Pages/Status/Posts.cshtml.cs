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
using Microsoft.Extensions.Configuration;

namespace Dev_Blog.Pages.Status
{
    public class PostsModel : BasePage
    {
        private readonly IPost _post;
        private readonly IConfiguration _config;

        [BindProperty]
        public List<Post> Posts { get; set; }

        [BindProperty]
        public string AdminUser { get; set; }

        public PostsModel(SignInManager<User> signInManager, UserManager<User> userManager, IPost post, IConfiguration config) : base(signInManager, userManager)
        {
            _config = config;
            _post = post;
        }

        public async Task<IActionResult> OnGet()
        {
            AdminUser = _config["AdminUserName"];
            Posts = await _post.GetAllPosts();
            return Page();
        }
    }
}