using DevBlog_BlazorServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBlog_BlazorServer.Interfaces
{
    public interface IPostService
    {
        Task<Dictionary<int, PostModel>> GetPosts();
    }
}