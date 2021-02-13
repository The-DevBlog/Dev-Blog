using BlazorServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorServer.Interfaces
{
    public interface IComments
    {
        Task<CommentModel> Create(CommentModel comment);

        Task<List<CommentModel>> GetComments();
    }
}