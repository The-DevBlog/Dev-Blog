using Dev_Blog.Models;
using Dev_Blog.Models.Interfaces;
using Dev_Blog.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Data
{
    public class DevBlogDbContext : DbContext
    {
        public DbSet<Post> Post { get; set; }
        public DbSet<LoginVM> VM { get; set; }

        public DevBlogDbContext(DbContextOptions<DevBlogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}