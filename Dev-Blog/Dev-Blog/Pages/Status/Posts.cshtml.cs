using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Dev_Blog.Models;
using Dev_Blog.Models.Base;
using Dev_Blog.Models.Interfaces;
using Dev_Blog.Models.ViewModels;
using Dropbox.Api.CloudDocs;
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

        public PostsModel(IPost post, IConfiguration config, IComment comment, SignInManager<User> signInManager) : base(signInManager)
        {
            _comment = comment;
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