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
    public class AddModel : BasePage
    {
        private readonly IImage _image;
        private readonly IPost _post;

        [BindProperty]
        public Post Post { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public AddModel(SignInManager<User> signInManager, UserManager<User> userManager, IImage image, IPost post) : base(signInManager, userManager)
        {
            _image = image;
            _post = post;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            string ext = Path.GetExtension(Image.FileName);
            string imgName = $"{Name}{ext}";
            await _post.Create(Post, imgName);

            // convert image to a stream
            if (Image != null)
            {
                using (var stream = new MemoryStream())
                {
                    await Image.CopyToAsync(stream);
                    var bytes = stream.ToArray();
                    await _image.UploadFile("pictures", imgName, bytes, Image.ContentType);
                }

                await _image.Upload(Image, imgName);
            }
            return RedirectToPage("Posts");
        }
    }
}