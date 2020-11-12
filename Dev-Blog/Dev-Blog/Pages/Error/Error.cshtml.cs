using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev_Blog.Models;
using Dev_Blog.Models.Base;
using Dev_Blog.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dev_Blog.Pages.Error
{
    public class ErrorModel : BasePage
    {
        public ErrorModel(IEmail email, SignInManager<User> signInManager, UserManager<User> userManager) : base(signInManager, userManager, email)
        {
        }
    }
}