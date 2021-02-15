using BlazorServer.Interfaces;
using BlazorServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static BlazorServer.Pages.Register;

namespace BlazorServer.Controllers
{
    public class AccountController : ControllerBase
    {
        public SignInManager<UserModel> SignInMgr { get; }
        public UserManager<UserModel> UserMgr { get; }

        private readonly IEmails _email;

        public AccountController(SignInManager<UserModel> sm, UserManager<UserModel> um, IEmails email)
        {
            _email = email;
            UserMgr = um;
            SignInMgr = sm;
        }

        [HttpPost("/signin")]
        public async Task<IActionResult> SignIn([FromForm] string username, [FromForm] string password)
        {
            var res = await SignInMgr.PasswordSignInAsync(username, password, true, lockoutOnFailure: true);
            if (res.Succeeded)
                return Redirect("/");

            return Redirect("/signin/attempt");
        }

        [HttpPost("/signout")]
        public async Task<IActionResult> LogOut()
        {
            await SignInMgr.SignOutAsync();
            return Redirect("/");
        }

        [HttpPost("/signup")]
        public async Task<IActionResult> Register([FromForm] RegisterVM registerVM)
        {
            var user = new UserModel()
            {
                Subscribed = true,
                UserName = registerVM.UserName,
                Email = registerVM.Email
            };

            var res = await UserMgr.CreateAsync(user, registerVM.Password);
            if (res.Succeeded)
            {
                await _email.Welcome(registerVM.Email);

                var curUser = await UserMgr.FindByNameAsync(user.UserName);
                var roleResult = UserMgr.AddToRoleAsync(curUser, "Visitor");
                roleResult.Wait();

                var signIn = await SignInMgr.PasswordSignInAsync(user.UserName, registerVM.Password, true, lockoutOnFailure: true);

                if (signIn.Succeeded)
                    return Redirect("/");
            }

            return Redirect("/signin/attempt");
        }
    }
}