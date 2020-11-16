using System;
using System.Collections.Generic;
using System.Linq;
using Dev_Blog.Models;
using Dev_Blog.Models.Base;
using Microsoft.AspNetCore.Identity;

namespace Dev_Blog.Pages.Bio
{
    public class BioModel : BasePage
    {
        public BioModel(SignInManager<User> signInManager) : base(signInManager)
        {
        }

        public void OnGet()
        {
        }
    }
}