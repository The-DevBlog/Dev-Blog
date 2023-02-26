using devblog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace devblog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        public SignInManager<User> SignInMgr { get; }
        public UserManager<User> UserMgr { get; }

        public IConfiguration _config;

        //private readonly IEmailRepository _email;

        public AccountsController(SignInManager<User> signInMgr, UserManager<User> usermgr, IConfiguration config)
        {
            UserMgr = usermgr;
            SignInMgr = signInMgr;
            _config = config;
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

        private SymmetricSecurityKey GenerateKey()
        {
            var key = new byte[32];
            using (var rsa = new RSACryptoServiceProvider())
                rsa.Encrypt(key, false);

            string keyStr = Convert.ToBase64String(key);
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr));

            return authKey;
        }

        private JwtSecurityToken GenerateToken(List<Claim> claims)
        {
            var key = GenerateKey();

            var token = new JwtSecurityToken(
                issuer: "https://localhost:44482/",
                audience: "https://localhost:44482/api/",
                expires: DateTime.Now.AddHours(3),
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignIn signIn)
        {
            var user = await UserMgr.FindByNameAsync(signIn.Username);

            if (user == null || !await UserMgr.CheckPasswordAsync(user, signIn.Password))
            {
                return BadRequest(new { error = "Invalid username or password" });
            }
            else
            {
                //var res = await SignInMgr.PasswordSignInAsync(signIn.Password, signIn.Username, true, false);
                await SignInMgr.PasswordSignInAsync(user, signIn.Password, true, false);
                var userRoles = await UserMgr.GetRolesAsync(user);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, signIn.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var userRole in userRoles)
                    claims.Add(new Claim(ClaimTypes.Role, userRole));


                var token = GenerateToken(claims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
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
                await UserMgr.AddToRoleAsync(currentUser, "Visitor");
                await SignInMgr.PasswordSignInAsync(user.UserName, user.PasswordHash, true, false);
            }

            return res;
        }
    }
}