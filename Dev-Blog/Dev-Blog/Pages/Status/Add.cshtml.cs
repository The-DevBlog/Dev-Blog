using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Dev_Blog.Models;
using Dev_Blog.Models.Base;
using Dev_Blog.Models.Interfaces;
using Dev_Blog.Models.ViewModels;
using Dropbox.Api;
using Dropbox.Api.Files;
using Dropbox.Api.Team;
using ECommerce.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Azure;

namespace Dev_Blog.Pages.Status
{
    [Authorize(Policy = "Admin")]
    [BindProperties]
    public class AddModel : BasePage
    {
        private readonly IImage _image;
        private readonly IPost _post;
        private readonly IEmail _email;

        public Post Post { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }

        public AddModel(IEmail email, SignInManager<User> signInManager, UserManager<User> userManager, IImage image, IPost post) : base(signInManager, userManager)
        {
            _email = email;
            _image = image;
            _post = post;
        }

        public async Task<IActionResult> OnPost()
        {
            string ext = Path.GetExtension(Image.FileName);
            string imgName = $"{DateTime.Now.Ticks}{Name}{ext}";

            if (Image != null)
            {
                var url = await _image.Upload(Image, imgName);
                await _post.Create(Post, url);
            }

            // email subscribed users
            await _email.NewPost();

            return RedirectToPage("Posts");
        }
    }
}