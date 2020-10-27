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
        public DbSet<Vote> Vote { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vote>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.UserId });
            });

            //modelBuilder.Entity<Comment>(entity =>
            //{
            //    entity.HasKey(e => e.Id);
            //});

            //modelBuilder.Entity<Post>(entity =>
            //{
            //    entity.HasKey(e => e.Id);
            //});
        }
    }
}