using Dev_Blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Data
{
    public class UserDevBlogDbContext : IdentityDbContext<User>
    {
        public UserDevBlogDbContext(DbContextOptions<UserDevBlogDbContext> options) : base(options)
        {
        }
    }
}