using devblog.Models;

namespace devblog.Interfaces
{
    public interface IVoteService
    {
        /// <summary>
        /// Adds an up vote
        /// </summary>
        /// <param name="vote"></param>
        /// <returns>UpVote</returns>
        Task<UpVote> UpVote(int postId, string username);

        /// <summary>
        /// Adds a down vote
        /// </summary>
        /// <param name="vote"></param>
        /// <returns>DownVote</returns>
        Task<DownVote> DownVote(int postId, string username);
    }
}
