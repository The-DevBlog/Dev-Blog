using Identity.Dapper.Entities;

namespace DevBlog_BlazorServer.Models
{
    public class Role : DapperIdentityRole
    {
        public const string Admin = "Admin";
    }
}