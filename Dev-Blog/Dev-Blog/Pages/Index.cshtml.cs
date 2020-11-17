using System.Threading.Tasks;
using Dev_Blog.Models;
using Dev_Blog.Models.Base;
using Dev_Blog.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dev_Blog.Pages
{
    [BindProperties]
    public class IndexModel : BasePage
    {
        private readonly IPost _post;
        public Post Post { get; set; }
        public string Context { get; set; }

        public IndexModel(IEmail email, IPost post, SignInManager<User> signInManager, UserManager<User> userManager) : base(signInManager, userManager, email)
        {
            _post = post;
        }

        public async Task<IActionResult> OnGet()
        {
            Post = await _post.GetLatestPost();
            return Page();
        }
    }
}