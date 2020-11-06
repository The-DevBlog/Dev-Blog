using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev_Blog.Models;
using Dev_Blog.Models.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dev_Blog.Pages.Account
{
    public class LoginErrorModel : BasePage
    {
        public LoginErrorModel(SignInManager<User> signInManager) : base(signInManager)
        {
        }

        public void OnGet()
        {
        }
    }
}