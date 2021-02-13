using BlazorServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BlazorServer.Pages.Posts;

namespace BlazorServer.Interfaces
{
    public interface IComments
    {
        Task<CommentModel> Create(CommentVM comment);

        Task<List<CommentModel>> GetComments();
    }
}