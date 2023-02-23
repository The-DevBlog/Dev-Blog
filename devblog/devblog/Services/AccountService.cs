using devblog.Interfaces;
using devblog.Models;
using Microsoft.AspNetCore.Identity;

namespace devblog.Services
{
    public class AccountService : IAccountService
    {
        public SignInManager<User> SignInMgr { get; }
        public UserManager<User> UserMgr { get; }
        //private readonly IEmailRepository _email;

        public AccountService(SignInManager<User> signInMgr, UserManager<User> usermgr)
        {
            UserMgr = usermgr;
            SignInMgr = signInMgr;
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user">New user to add</param>
        /// <returns>Task<IdentityResult></returns>
        public async Task<IdentityResult> Create(User user)
        {
            var res = await UserMgr.CreateAsync(user, user.PasswordHash);
            if (res.Succeeded)
            {
                //await _email.Welcome(registerVM.Email);
                var currentUser = await UserMgr.FindByNameAsync(user.UserName);
                var roleResult = UserMgr.AddToRoleAsync(currentUser, "Visitor");
                roleResult.Wait();

                await SignInMgr.PasswordSignInAsync(user.UserName, user.PasswordHash, true, lockoutOnFailure: true);
            }

            return res;
        }
    }
}
