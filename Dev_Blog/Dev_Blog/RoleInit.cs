using Dev_Blog.Data;
using Dev_Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dev_Blog
{
    //TODO: get rid of class?
    public class RoleInit
    {
        private static readonly List<IdentityRole> Roles = new List<IdentityRole>()
        {
            new IdentityRole{Name = RoleModel.Admin, NormalizedName = RoleModel.Admin.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString()},
        };

        public static void SeedData(IServiceProvider serviceProvider, UserManager<UserModel> userManager, IConfiguration _config)
        {
            using (var dbContext = new UserDbContext(serviceProvider.GetRequiredService<DbContextOptions<UserDbContext>>()))
            {
                //dbContext.Database.EnsureCreated();
                AddRoles(dbContext);
                SeedUsers(userManager, _config);
            }
        }

        public static void AddRoles(UserDbContext dbContext)
        {
            if (dbContext.Roles.Any()) return;
            foreach (var role in Roles)
            {
                dbContext.Roles.Add(role);
                dbContext.SaveChanges();
            }
        }

        public static void SeedUsers(UserManager<UserModel> userManager, IConfiguration _config)
        {
            string username = Environment.GetEnvironmentVariable("ADMIN_USERNAME", EnvironmentVariableTarget.User);
            if (userManager.FindByNameAsync(username).Result == null)
            {
                UserModel user = new UserModel()
                {
                    Email = Environment.GetEnvironmentVariable("ADMIN_EMAIL", EnvironmentVariableTarget.User),
                    UserName = username
                };

                string password = Environment.GetEnvironmentVariable("ADMIN_PASSWORD", EnvironmentVariableTarget.User);
                IdentityResult result = userManager.CreateAsync(user, password).Result;
                if (result.Succeeded)
                {
                    //Claim userName = new Claim("UserName", user.UserName);
                    //Claim email = new Claim("Email", user.Email);

                    //var e = userManager.AddClaimAsync(user, email).Result;
                    //var u = userManager.AddClaimAsync(user, userName).Result;
                    userManager.AddToRoleAsync(user, RoleModel.Admin).Wait();
                }
            }
        }
    }
}