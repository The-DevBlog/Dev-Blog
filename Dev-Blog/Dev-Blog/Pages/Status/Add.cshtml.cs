using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dev_Blog.Models;
using Dev_Blog.Models.Interfaces;
using Dev_Blog.Models.ViewModels;
using ECommerce.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dev_Blog.Pages.Status
{
    [Authorize(Policy = "Admin")]
    public class AddModel : PageModel
    {
        private readonly IImage _image;
        private readonly IPost _post;
        private readonly SignInManager<User> _signInManager;

        [BindProperty]
        public LoginVM Login { get; set; }

        [BindProperty]
        public Post Post { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public AddModel(SignInManager<User> signInManager, IImage image, IPost post)
        {
            _image = image;
            _post = post;
            _signInManager = signInManager;
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
            }
            return RedirectToPage("Index");
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