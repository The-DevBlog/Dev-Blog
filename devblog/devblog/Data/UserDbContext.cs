using devblog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace devblog.Data
{
    public class UserDbContext : IdentityDbContext<UserModel>
    {
        public UserDbContext(DbContextOptions opt) : base(opt)
        {
        }
    }
}