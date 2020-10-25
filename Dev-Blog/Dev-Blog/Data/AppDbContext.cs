using Dev_Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<UpVote> UpVote { get; set; }
        public DbSet<DownVote> DownVote { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UpVote>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.UserId });
            });

            modelBuilder.Entity<DownVote>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.UserId });
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Date);
                entity.Property(e => e.Content);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UpdateNum);
                entity.Property(e => e.Date);
                entity.Property(e => e.ImgURL);
                entity.Property(e => e.Description);
            });
        }
    }
}