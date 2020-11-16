using Dev_Blog.Models;
using Microsoft.EntityFrameworkCore;

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
        }
    }
}