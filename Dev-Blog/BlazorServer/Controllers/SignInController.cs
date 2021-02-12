using BlazorServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Controllers
{
    public class SignInController : ControllerBase
    {
        public UserManager<UserModel> UserMgr { get; }
        public SignInManager<UserModel> SignInMgr { get; }

        public SignInController(SignInManager<UserModel> sm, UserManager<UserModel> um)
        {
            UserMgr = um;
            SignInMgr = sm;
        }

        [HttpPost("/signin")]
        public async Task<IActionResult> Index([FromForm] string username, [FromForm] string password)
        {
            var user = UserMgr.Users.Where(x => x.UserName == username).FirstOrDefault();
            var count = UserMgr.Users.Count();

            //await SignInMgr.SignInAsync()
            var result = await SignInMgr.PasswordSignInAsync(username, password, true, lockoutOnFailure: true);
            return Redirect("/");
        }
    }
}