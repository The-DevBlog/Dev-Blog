using Microsoft.AspNetCore.Mvc;

namespace devblog.Interfaces
{
    public interface IVoteService
    {
        /// <summary>
        /// Returns the number of upvotes for specific post
        /// </summary>
        /// <param name="id">id of post</param>
        /// <returns>number of upvotes</returns>
        Task<int> GetUpVotesForPost(int id);

        /// <summary>
        /// Returns the number of upvotes for specific post
        /// </summary>
        /// <param name="id">id of post</param>
        /// <returns>number of upvotes</returns>
        Task<int> GetDownVotesForPost(int id);

        /// <summary>
        /// Adds an up vote
        /// </summary>
        /// <param name="vote"></param>
        /// <returns>Number of votes for post</returns>
        Task<int> UpVote(int postId, string username);

        /// <summary>
        /// Adds a down vote
        /// </summary>
        /// <param name="vote"></param>
        /// <returns>Number of votes for post</returns>
        Task<int> DownVote(int postId, string username);
    }
}
