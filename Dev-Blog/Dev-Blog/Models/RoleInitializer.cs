using Dev_Blog.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dev_Blog.Models
{
    public class RoleInitializer
    {
        private static readonly List<IdentityRole> Roles = new List<IdentityRole>()
        {
            new IdentityRole{Name = Role.Admin, NormalizedName = Role.Admin.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString()},
        };

        public static async Task SeedData(IServiceProvider serviceProvider, UserManager<User> userManager, IConfiguration _config)
        {
            using (var dbContext = new UserDevBlogDbContext(serviceProvider.GetRequiredService<DbContextOptions<UserDevBlogDbContext>>()))
            {
                dbContext.Database.EnsureCreated();
                AddRoles(dbContext);
                SeedUsers(userManager, _config);
            }
        }

        public static void AddRoles(UserDevBlogDbContext dbContext)
        {
            if (dbContext.Roles.Any()) return;
            foreach (var role in Roles)
            {
                dbContext.Roles.Add(role);
                dbContext.SaveChanges();
            }
        }

        public static void SeedUsers(UserManager<User> userManager, IConfiguration _config)
        {
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();

                //    Post post = new Post
                //    {
                //        UpdateNum = "1",
                //        Date = DateTime.Now,
                //        ImgURL = "thisIsATest.com",
                //        Description = "this test is so totally going to work on the first attempt."
                //    };
                //    context.Post.Add(post);

                //    User user = new User
                //    {
                //        Email = _config["AdminEmail"],
                //        UserName = _config["AdminUserName"]
                //    };
                //    context.User.Add(user);
                context.SaveChangesAsync();
            }

            //using (var context = new UserDbContext())
            //{
            //    context.Database.EnsureCreated();
            //    if (userManager.FindByNameAsync(_config["AdminEmail"]).Result == null)
            //    {
            //        User user = new User()
            //        {
            //            Email = _config["AdminEmail"],
            //            UserName = _config["AdminUserName"]
            //        };

            //        IdentityResult result = userManager.CreateAsync(user, _config["AdminPassword"]).Result;
            //        if (result.Succeeded)
            //        {
            //            Claim userName = new Claim("UserName", user.UserName);
            //            Claim email = new Claim("Email", user.Email);

            //            var e = userManager.AddClaimAsync(user, email).Result;
            //            var u = userManager.AddClaimAsync(user, userName).Result;
            //            userManager.AddToRoleAsync(user, Role.Admin).Wait();
            //        }
            //    }
            //    context.SaveChangesAsync();
            //}

            if (userManager.FindByNameAsync(_config["AdminEmail"]).Result == null)
            {
                User user = new User()
                {
                    Email = _config["AdminEmail"],
                    UserName = _config["AdminUserName"]
                };

                IdentityResult result = userManager.CreateAsync(user, _config["AdminPassword"]).Result;
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