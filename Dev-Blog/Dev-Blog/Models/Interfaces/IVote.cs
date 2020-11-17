using System.Threading.Tasks;

namespace Dev_Blog.Models.Interfaces
{
    public interface IVote
    {
        /// <summary>
        /// Adds a vote to the database
        /// </summary>
        /// <param name="vote">The vote to add</param>
        /// <returns>Successful completion of task</returns>
        public Task CreateUpVote(Vote vote);

        /// <summary>
        /// Adds aa vote to the database
        /// </summary>
        /// <param name="vote">The vote to add</param>
        /// <returns>Successful completion of task</returns>
        public Task CreateDownVote(Vote vote);

        ///// <summary>
        ///// Removes a specified vote from the database
        ///// </summary>
        ///// <param name="vote">The vote to remove</param>
        ///// <returns>Successful completion of task</returns>
        public Task DeleteVote(Vote vote);

        /// <summary>
        /// Checks to see if the user has already upvoted on a specified post
        /// </summary>
        /// <param name="vote">The vote to check for</param>
        /// <returns>A boolean indicating whether the vote exists within the database</returns>
        public Task<bool> HasUpVoted(Vote vote);

        /// <summary>
        /// Checks to see if the user has already downvoted on a specified post
        /// </summary>
        /// <param name="vote">The vote to check for</param>
        /// <returns>A boolean indicating whether the vote exists within the database</returns>
        public Task<bool> HasDownVoted(Vote vote);

        /// <summary>
        /// Retrieves a specified vote from the database
        /// </summary>
        /// <param name="vote">The specified vote</param>
        /// <returns>The specified vote</returns>
        public Task<Vote> GetVote(Vote vote);
    }
}