using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dev_Blog.Pages.Status
{
    public class AddModel : PageModel
    {
        private readonly IImage _image;

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public AddModel(IImage image)
        {
            _image = image;
        }

        public void OnGet()
        {
        }

        public async Task OnPost()
        {
            string ext = Path.GetExtension(Image.FileName);
            // Goal: send the uploaded image to blob
            // convert image to a stream
            if (Image != null)
            {
                using (var stream = new MemoryStream())
                {
                    await Image.CopyToAsync(stream);
                    var bytes = stream.ToArray();
                    await _image.UploadFile("pictures", $"{Name}{ext}", bytes, Image.ContentType);
                }
            }
        }
    }
}