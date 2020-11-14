using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Dev_Blog.Models;
using Dev_Blog.Models.Base;
using Dev_Blog.Models.Interfaces;
using Dev_Blog.Models.ViewModels;
using Dev_Blog.Pages.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dev_Blog.Pages
{
    [BindProperties]
    public class IndexModel : BasePage
    {
        private readonly IPost _post;
        private readonly IEmail _email;

        public Post Post { get; set; }
        public string Context { get; set; }

        public IndexModel(IEmail email, IPost post, SignInManager<User> signInManager, UserManager<User> userManager) : base(signInManager, userManager, email)
        {
            _email = email;
            _post = post;
        }

        public async Task<IActionResult> OnGet()
        {
            Post = await _post.GetLatestPost();
            return Page();
        }
    }
}