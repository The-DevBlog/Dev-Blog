using System.Threading.Tasks;
using Dev_Blog.Models;
using Dev_Blog.Models.Base;
using Dev_Blog.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dev_Blog.Pages.Status
{
    [BindProperties]
    [Authorize(Policy = "Admin")]
    public class EditModel : BasePage
    {
        public EditModel(IPost post, SignInManager<User> signInManager, UserManager<User> userManager) : base(signInManager, userManager)
        {
            _post = post;
        }

        private readonly IPost _post;

        public Post Post { get; set; }
        public string Description { get; set; }
        public string UpdateNum { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            Post = await _post.GetPost(id);
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            Post = await _post.GetPost(Post.Id);
            await _post.Edit(Post, Description, UpdateNum);
            return RedirectToPage("Posts");
        }
    }
}