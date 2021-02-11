using BlazorServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorServer.Interfaces
{
    internal interface IPosts
    {
        Task<List<PostModel>> GetPosts();
    }
}