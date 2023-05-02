using devblog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace devblog.Data
{
    public class UserDbContext : IdentityDbContext<User>
    {
        public DbSet<Username> usernames { get; set; }

        public UserDbContext(DbContextOptions opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Username>(entity =>
            {
                entity.HasKey(e => e.Name);
            });
        }
    }
}