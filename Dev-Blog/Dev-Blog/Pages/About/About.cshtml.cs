using System;
using System.Collections.Generic;
using System.Linq;
using Dev_Blog.Models;
using Dev_Blog.Models.Base;
using Microsoft.AspNetCore.Identity;

namespace Dev_Blog.Pages.About
{
    public class AboutModel : BasePage
    {
        public AboutModel(SignInManager<User> signInManager) : base(signInManager)
        {
        }
    }
}