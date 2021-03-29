using BlazorServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BlazorServer.Pages.Posts;

namespace BlazorServer.Interfaces
{
    public interface ICommentRepository
    {
        Task<CommentModel> Create(CommentVM comment);

        Task Update(CommentModel comment);

        Task<List<CommentModel>> GetComments();

        Task<CommentModel> GetComment(int id);

        Task Delete(int id);
    }
}