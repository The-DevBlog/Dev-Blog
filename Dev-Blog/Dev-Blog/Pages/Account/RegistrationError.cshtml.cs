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

namespace Dev_Blog.Pages.Account
{
    public class RegistrationErrorModel : BasePage
    {
        public RegistrationErrorModel(IEmail email, SignInManager<User> signInManager, UserManager<User> userManager) : base(signInManager, userManager, email)
        {
        }
    }
}