using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dev_Blog.Models;
using Dev_Blog.Models.Interfaces;
using ECommerce.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dev_Blog.Pages.Status
{
    public class AddModel : PageModel
    {
        private readonly IImage _image;
        private readonly INewPost _newPost;

        [BindProperty]
        public NewPost NewPost { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public AddModel(IImage image, INewPost newPost)
        {
            _image = image;
            _newPost = newPost;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            string ext = Path.GetExtension(Image.FileName);
            string imgName = $"{Name}{ext}";
            await _newPost.Create(NewPost, imgName);

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
            //https://<storage_account_name>.blob.core.windows.net/<container_name>/<blob_name>
            return RedirectToPage("Index");
        }
    }
}