using DevBlog_BlazorServer.Models;
using Identity.Dapper.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBlog_BlazorServer.Interfaces
{
    public interface IPostService
    {
        Task<Dictionary<int, PostModel>> GetPosts();

        Task<List<IdentityUser>> GetUsers();

        Task<bool> Login(LoginModel model);
    }
}