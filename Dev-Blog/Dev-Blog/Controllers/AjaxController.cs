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
    public class AjaxController : Controller
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

        //[HttpPost]
        //public ActionResult PostComment(Comment comment)
        //{
        //    //string id = _userManager.GetUserId(User);
        //    //string userName = HttpContext.User.Identity.Name;
        //    //return await _post.GetPost(postId);
        //    //await _comment.Create(id, userName);
        //    //return Json(new { success = true });
        //    return View();
        //}

        public void Post(Comment comment)
        {
        }
    }
}