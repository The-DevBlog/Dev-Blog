using devblog.Interfaces;
using devblog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace devblog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        public SignInManager<User> SignInMgr { get; }
        public UserManager<User> UserMgr { get; }

        //private readonly IEmailRepository _email;

        public AccountsController(SignInManager<User> signInMgr, UserManager<User> usermgr)
        {
            UserMgr = usermgr;
            SignInMgr = signInMgr;
        }

        //[HttpPost("/deleteAccount")]
        //public async Task<IActionResult> DeleteAccount()
        //{
        //    // get current user
        //    var username = User.Identity.Name.ToUpper();
        //    User user = UserMgr.Users.Where(x => x.NormalizedUserName == username).FirstOrDefault();

        //    // sign current user out and delete account
        //    await SignInMgr.SignOutAsync();
        //    await UserMgr.DeleteAsync(user);

        //    return Redirect("/");
        //}

        //[HttpGet("{user}")]
        //public async Task<string> GetUserName(User user)
        //{
        //    var res = await UserMgr.GetUserNameAsync(user);
        //    return res;
        //}

        [HttpPost("signin")]
        public async Task<Microsoft.AspNetCore.Identity.SignInResult> SignIn([FromForm] string username, [FromForm] string password)
        {
            var res = await SignInMgr.PasswordSignInAsync(password, username, true, false);
            return res;
            //var res = await SignInMgr.PasswordSignInAsync(username, password, true, lockoutOnFailure: true);
            //if (res.Succeeded)
            //    return Redirect("/");

            //return Redirect("/signin/attempt");
        }

        //[HttpPost("/signout")]
        //public async Task<IActionResult> LogOut()
        //{
        //    await SignInMgr.SignOutAsync();
        //    return Redirect("/");
        //}

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user">New user to add</param>
        /// <returns>Task<IdentityResult></returns>
        [HttpPost]
        public async Task<IdentityResult> Create(User user)
        {
            var res = await UserMgr.CreateAsync(user, user.PasswordHash);
            if (res.Succeeded)
            {
                //await _email.Welcome(registerVM.Email);
                var currentUser = await UserMgr.FindByNameAsync(user.UserName);
                var roleResult = await UserMgr.AddToRoleAsync(currentUser, "Visitor");
                var signInRes = await SignInMgr.PasswordSignInAsync(user.UserName, user.PasswordHash, true, lockoutOnFailure: true);
            }

            return res;
        }
    }
}