using BlazorServer.Models;
using System.Threading.Tasks;

namespace BlazorServer.Interfaces
{
    public interface IVoteRepository
    {
        Task<UpVoteModel> UpVote(int postId, string username);

        Task<DownVoteModel> DownVote(int postId, string username);

        Task<UpVoteModel> GetUpVote(int postId, string username);

        Task<DownVoteModel> GetDownVote(int postId, string username);
    }
}