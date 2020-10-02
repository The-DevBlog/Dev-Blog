using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev_Blog.Models;
using Dev_Blog.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dev_Blog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IPost _post;

        [BindProperty]
        public Post Post { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IPost post)
        {
            _post = post;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            Post = await _post.GetLatestPost();
            return Page();
        }
    }
}