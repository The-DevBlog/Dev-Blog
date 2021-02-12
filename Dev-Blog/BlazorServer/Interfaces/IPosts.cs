using BlazorServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorServer.Interfaces
{
    internal interface IPosts
    {
        Task<PostModel> Create(PostModel post, string url);

        Task<List<PostModel>> GetPosts();
    }
}