using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        private readonly IComment _comment;
        private readonly UserManager<User> _userManager;

        [BindProperty]
        public List<Post> Posts { get; set; }

        [BindProperty]
        public Post Post { get; set; }

        [BindProperty]
        public string Comment { get; set; }

        [BindProperty]
        public string AdminUser { get; set; }

        public PostsModel(UserManager<User> userManager, IPost post, IConfiguration config, IComment comment) : base(userManager)
        {
            _comment = comment;
            _userManager = userManager;
            _config = config;
            _post = post;
        }

        public async Task<IActionResult> OnGet()
        {
            AdminUser = _config["AdminUserName"];
            Posts = await _post.GetAllPosts();
            return Page();
        }

        public async Task<IActionResult> OnPostComment()
        {
            var post = await _post.GetCurrentPost(Post.Id);
            var user = await _userManager.GetUserAsync(User.Identity.Name);
            await _comment.Create(user.Id, Post.Id, Content);
            return Page();
        }
    }
}