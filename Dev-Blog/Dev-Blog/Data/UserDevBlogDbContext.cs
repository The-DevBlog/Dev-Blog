using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Data
{
    public class UserDevBlogDbContext : DbContext
    {
        public UserDevBlogDbContext(DbContextOptions<UserDevBlogDbContext> options) : base(options)
        {
        }
    }
}