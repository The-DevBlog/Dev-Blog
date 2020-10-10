using Dev_Blog.Models;
using Dev_Blog.Models.Interfaces;
using Dev_Blog.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Data
{
    public class DevBlogDbContext : DbContext
    {
        public DbSet<Post> Post { get; set; }
        public IConfiguration Configuration { get; }

        public DevBlogDbContext(DbContextOptions<DevBlogDbContext> options, IConfiguration config) : base(options)
        {
            Configuration = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Configuration.GetConnectionString("DevBlogDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UpdateNum).IsRequired();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.ImgURL);
                entity.Property(e => e.Description);
            });
        }
    }
}