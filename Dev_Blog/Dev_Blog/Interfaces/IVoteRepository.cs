using Dev_Blog.Models;
using System.Threading.Tasks;

namespace Dev_Blog.Interfaces
{
    public interface IVoteRepository
{
    /// <summary>
    /// Adds an up vote
    /// </summary>
    /// <param name="vote"></param>
    /// <returns>Successful completion of task</returns>
    Task<UpVoteModel> UpVote(int postId, string username);

    /// <summary>
    /// Adds a down vote
    /// </summary>
    /// <param name="vote"></param>
    /// <returns>Successful completion of task</returns>
    Task<DownVoteModel> DownVote(int postId, string username);
}
}