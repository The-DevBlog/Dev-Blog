using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dev_Blog.Models;
using Dev_Blog.Models.Base;
using Dev_Blog.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Dev_Blog.Pages.Status
{
    [BindProperties]
    public class PostsModel : BasePage
    {
        private readonly IPost _post;
        private readonly IConfiguration _config;
        private readonly IComment _comment;

        public int PageNumber { get; set; }
        public int LastPage { get; set; }
        public List<Post> Posts { get; set; }
        public Post Post { get; set; }
        public List<Comment> Comments { get; set; }
        public Comment Comment { get; set; }
        public string AdminUser { get; set; }

        public PostsModel(IPost post, IConfiguration config, IComment comment, SignInManager<User> signInManager, UserManager<User> userManager) : base(signInManager, userManager)
        {
            _comment = comment;
            _config = config;
            _post = post;
            AdminUser = _config["AdminUserName"];
        }

        public async Task<IActionResult> OnGet()
        {
            // if user does not have page tracking cookie, create one
            int pageIdx;
            if (Request.Cookies["pageTracker"] == null)
                pageIdx = AddCookie(1);
            else
                pageIdx = int.Parse(Request.Cookies["pageTracker"]);

            PageNumber = pageIdx;
            LastPage = await _post.GetLastPage();
            Posts = await _post.GetPage(pageIdx);

            return Page();
        }

        public async Task<IActionResult> OnGetPageLeft()
        {
            // get previous page number
            int pageIdx = int.Parse(Request.Cookies["pageTracker"]) - 1;

            if (pageIdx >= 1)
            {
                AddCookie(pageIdx);
                Posts = await _post.GetPage(pageIdx);
            }
            else
                Posts = await _post.GetPage(++pageIdx);

            PageNumber = pageIdx;

            return Page();
        }

        public async Task<IActionResult> OnGetPageRight()
        {
            // get next page number
            int pageIdx = int.Parse(Request.Cookies["pageTracker"]) + 1;

            if (await _post.CanPageRight(pageIdx))
            {
                AddCookie(pageIdx);
                Posts = await _post.GetPage(pageIdx);
            }
            else
                Posts = await _post.GetPage(--pageIdx);

            LastPage = await _post.GetLastPage();
            PageNumber = pageIdx;

            return Page();
        }

        public async Task<IActionResult> OnGetLastPage()
        {
            LastPage = await _post.GetLastPage();
            Posts = await _post.GetPage(LastPage);
            PageNumber = LastPage;
            AddCookie(LastPage);

            return Page();
        }

        public async Task<IActionResult> OnGetFirstPage()
        {
            Posts = await _post.GetPage(1);
            PageNumber = 1;
            AddCookie(1);

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

        // cookie to keep track of current page
        public int AddCookie(int page)
        {
            string key = "pageTracker";
            string val = page.ToString();
            CookieOptions cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Append(key, val, cookie);

            return int.Parse(val);
        }
    }
}