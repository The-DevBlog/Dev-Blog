using BlazorServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorServer.Interfaces
{
    internal interface IPosts
    {
        //TODO: Summary comments
        Task<PostModel> Create(PostModel post, string url);

        Task<List<PostModel>> GetPosts();

        Task<PostModel> GetPost(int postId);

        Task UpdatePost(PostModel post);
    }
}