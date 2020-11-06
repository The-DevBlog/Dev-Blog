using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev_Blog.Models;
using Dev_Blog.Models.Base;
using Dev_Blog.Models.Interfaces;
using Dev_Blog.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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