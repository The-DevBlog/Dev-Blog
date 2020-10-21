using Dev_Blog.Models;
using Dev_Blog.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Controllers
{
    [ApiController]
    public class AjaxController : ControllerBase
    {
        private readonly IComment _comment;
        private readonly IPost _post;
        private readonly UserManager<User> _userManager;

        public AjaxController(IPost post, IComment comment, UserManager<User> userManager)
        {
            _post = post;
            _userManager = userManager;
            _comment = comment;
        }

        [HttpPost("/PostComment")]
        public async Task<ActionResult> PostComment(Comment comment)
        {
            Post post = await _post.GetPost(comment.PostId);
            string id = _userManager.GetUserId(User);
            string userName = HttpContext.User.Identity.Name;
            await _comment.Create(id, post, comment.Content, userName);
            return Ok();
        }
    }
}