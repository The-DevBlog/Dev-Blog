using BlazorServer.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorServer.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<PostModel> Post { get; set; }
        public DbSet<CommentModel> Comment { get; set; }
        public DbSet<VoteModel> Vote { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VoteModel>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.UserId });
            });
        }
    }
}