using Dev_Blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dev_Blog.Data
{
    public class UserDbContext : IdentityDbContext<UserModel>
{
    public UserDbContext(DbContextOptions opt) : base(opt)
    {
    }
}
}