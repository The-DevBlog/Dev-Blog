using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Controllers
{
    public class AjaxController : Controller
    {
        [HttpGet]
        public IActionResult AjaxThing()
        {
            return View();
        }
    }
}