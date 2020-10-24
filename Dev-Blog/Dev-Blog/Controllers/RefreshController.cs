using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev_Blog.Models;
using Dev_Blog.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dev_Blog.Controllers
{
    public class RefreshController : Controller
    {
        private readonly IComment _comment;

        public RefreshController(IComment comment)
        {
            _comment = comment;
        }

        public async Task<ActionResult> Comments()
        {
            Comment comment = await _comment.GetLatestComment();

            return PartialView("_Test", comment);
        }
    }
}