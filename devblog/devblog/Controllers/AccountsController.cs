using devblog.Interfaces;
using devblog.Models;
using devblog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace devblog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        public SignInManager<User> _signInMgr { get; }
        public UserManager<User> _userMgr { get; }
        public IConfiguration _config;
        private readonly IUsernameService _username;
        private readonly IEmailService _email;

        public AccountsController(SignInManager<User> signInMgr, UserManager<User> usermgr, IConfiguration config, IUsernameService username, IEmailService email)
        {
            _userMgr = usermgr;
            _signInMgr = signInMgr;
            _config = config;
            _username = username;
            _email = email;
        }

        /// <summary>
        /// Returns all user's usernames and emails
        /// </summary>
        /// <returns>List<UserInfo></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("count")]
        public async Task<List<UserInfo>> GetUsersCount()
        {
            var users = await _userMgr.Users
                .Select(u => new UserInfo
                {
                    UserName = u.UserName,
                    Email = u.Email
                })
                .ToListAsync();

            return users;
        }

        public class UserInfo
        {
            public string? UserName { get; set; }
            public string? Email { get; set; }
        }


        /// <summary>
        /// Delete an account
        /// </summary>
        /// <param name="username"></param>
        [Authorize(Roles = "Visitor")]
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteAccount(string username)
        {
            // get current user
            User user = _userMgr.Users.Where(x => x.NormalizedUserName == username).FirstOrDefault();

            if (user != null)
            {
                // sign current user out and delete account
                await _signInMgr.SignOutAsync();
                await _userMgr.DeleteAsync(user);
                return Redirect("/");
            }

            return Redirect("/");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("admin/{username}")]
        public async Task AdminDeleteAccount(string username)
        {
            User user = _userMgr.Users.Where(x => x.NormalizedUserName == username).FirstOrDefault();

            if (user != null)
            {
                await _userMgr.DeleteAsync(user);
            }
        }


        /// <summary>
        /// Signs in a user
        /// </summary>
        /// <param name="signIn"></param>
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignIn signIn)
        {
            var user = await _userMgr.FindByNameAsync(signIn.Username);

            if (user == null || !await _userMgr.CheckPasswordAsync(user, signIn.Password))
            {
                return BadRequest(new { error = "Invalid userName or password" });
            }
            else
            {
                //var res = await _signInMgr.PasswordSignInAsync(signIn.Password, signIn.Username, true, false);
                var res = await _signInMgr.PasswordSignInAsync(user, signIn.Password, true, false);

                var claims = await GenerateClaims(user);
                var token = GenerateToken(claims);

                if (res.Succeeded)
                {
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                    });
                }
                else
                    return BadRequest(new { error = "Unable to sign in. Please try again" });

            }
        }

        /// <summary>
        /// Signs out the currently sign in user
        /// </summary>
        /// <returns>Task<IActionResult></returns>
        [Authorize]
        [HttpPost("signout")]
        public async Task<IActionResult> LogOut()
        {
            await _signInMgr.SignOutAsync();
            return Ok();
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user">New user to add</param>
        /// <returns>Task<IActionResult></returns>
        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            // verify unique username
            var userName = await _username.Exists(user.UserName.Normalize());

            if (userName)
            {
                var error = new IdentityError();
                error.Description = "Username already exists";

                return BadRequest(new { errors = new List<IdentityError>() { error } });
            }

            // verify unique email
            var email = _userMgr.Users.Where(x => x.NormalizedEmail == user.Email.Normalize()).FirstOrDefault();
            if (email != null)
            {
                var error = new IdentityError();
                error.Description = "Email already exists";

                return BadRequest(new { errors = new List<IdentityError>() { error } });
            }

            var res = await _userMgr.CreateAsync(user, user.PasswordHash);
            if (res.Succeeded)
            {
                var welcomeEmail = _email.Welcome(user.Email);
                var currentUser = await _userMgr.FindByNameAsync(user.UserName);

                await _userMgr.AddToRoleAsync(currentUser, "Visitor");
                await _signInMgr.PasswordSignInAsync(user.UserName, user.PasswordHash, true, false);
                await _username.Create(user.UserName.Normalize());

                var claims = await GenerateClaims(user);
                var token = GenerateToken(claims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            else
                return BadRequest(new { errors = res.Errors.ToList() });
        }


        private JwtSecurityToken GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("Key")));

            var token = new JwtSecurityToken(
                issuer: "https://localhost:44482/",
                audience: "https://localhost:44482/api/",
                expires: DateTime.Now.AddHours(3),
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        private async Task<List<Claim>> GenerateClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("userName", user.UserName),
                new Claim("email",  user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await _userMgr.GetRolesAsync(user);
            foreach (var userRole in userRoles)
                claims.Add(new Claim("role", userRole));
            //claims.Add(new Claim(ClaimTypes.Role, userRole));

            return claims;
        }
    }
}