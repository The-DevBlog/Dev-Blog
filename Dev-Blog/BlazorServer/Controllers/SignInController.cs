using BlazorServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorServer.Controllers
{
    public class SignInController : ControllerBase
    {
        public SignInManager<UserModel> SignInMgr { get; }

        public SignInController(SignInManager<UserModel> sm)
        {
            SignInMgr = sm;
        }

        [HttpPost("/signin")]
        public async Task<IActionResult> SignIn([FromForm] string username, [FromForm] string password)
        {
            var res = await SignInMgr.PasswordSignInAsync(username, password, true, lockoutOnFailure: true);
            if (res.Succeeded)
                return Redirect("/");

            return Redirect("/signin/attempt");

            //return Redirect("/");
        }
    }
}