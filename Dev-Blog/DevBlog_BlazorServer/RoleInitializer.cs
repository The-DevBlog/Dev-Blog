using DevBlog_BlazorServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DevBlog_BlazorServer
{
    public class RoleInitializer
    {
        private IConfiguration _config;

        public RoleInitializer(IConfiguration config)
        {
            _config = config;
        }

        private static readonly List<IdentityRole> Roles = new List<IdentityRole>()
        {
            new IdentityRole{Name = Role.Admin, NormalizedName = Role.Admin.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString()},
        };

        public static void AddRoles()
        {
            //string sql = "INSERT INTO people " +
            // "(FirstName, LastName) " +
            // "values (@FirstName, @LastName);";
            //string sql = "INSERT INTO "
        }

        public static void SeedUsers(UserManager<UserModel> userManager, IConfiguration _config)
        {
            if (userManager.FindByNameAsync(_config["AdminEmail"]).Result == null)
            {
                UserModel user = new UserModel()
                {
                    Email = _config["AdminEmail"],
                    UserName = _config["AdminUserName"]
                };

                var result = userManager.CreateAsync(user, _config["AdminPassword"]).Result;
                if (result.Succeeded)
                {
                    Claim userName = new Claim("UserName", user.UserName);
                    Claim email = new Claim("Email", user.Email);

                    var e = userManager.AddClaimAsync(user, email).Result;
                    var u = userManager.AddClaimAsync(user, userName).Result;
                    userManager.AddToRoleAsync(user, Role.Admin).Wait();
                }
            }
        }
    }
}