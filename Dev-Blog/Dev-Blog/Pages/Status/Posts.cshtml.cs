using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Dev_Blog.Models;
using Dev_Blog.Models.Base;
using Dev_Blog.Models.Interfaces;
using Dev_Blog.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IEmail _email;
        private readonly UserManager<User> _userManager;

        [BindProperty]
        public List<Post> Posts { get; set; }

        [BindProperty]
        public Post Post { get; set; }

        [BindProperty]
        public List<Comment> Comments { get; set; }

        [BindProperty]
        public Comment Comment { get; set; }

        [BindProperty]
        public string AdminUser { get; set; }

        public PostsModel(SignInManager<User> signInManager, UserManager<User> userManager, IEmail email, IPost post, IConfiguration config, IComment comment) : base(signInManager, userManager, email)
        {
            _email = email;
            _comment = comment;
            _userManager = userManager;
            _config = config;
            _post = post;
        }

        public async Task<IActionResult> OnGet()
        {
            AdminUser = _config["AdminUserName"];
            Posts = await _post.GetAllPosts();
            Comments = await _comment.GetAllComments();
            return Page();
        }

        public async Task<IActionResult> OnPostComment()
        {
            // get post being commented on
            var post = await _post.GetPost(Post.Id);

            // get id of current user
            string id = _userManager.GetUserId(User);

            // grab username of current user
            string userName = HttpContext.User.Identity.Name;

            await _comment.Create(id, post, Comment.Content, userName);

            return RedirectToPagePermanent("Posts");
        }

        public async Task<IActionResult> OnPostDeleteComment()
        {
            Comment comment = await _comment.GetComment(Comment.Id);
            await _comment.Delete(comment);
            return RedirectToPagePermanent("Posts");
        }

        public async Task<IActionResult> OnPostDeletePost()
        {
            // get post being deleted
            var post = await _post.GetPost(Post.Id);

            await _post.Delete(post);
            return RedirectToPagePermanent("Posts");
        }
    }
}