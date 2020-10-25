using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Models.Interfaces
{
    public interface IVote
    {
        /// <summary>
        /// Adds an upvote to the database
        /// </summary>
        /// <param name="vote">The vote to add</param>
        /// <returns>Successful completion of task</returns>
        public Task CreateUpVote(UpVote vote);

        /// <summary>
        /// Adds an downvote to the database
        /// </summary>
        /// <param name="vote">The vote to add</param>
        /// <returns>Successful completion of task</returns>
        public Task CreateDownVote(DownVote vote);

        /// <summary>
        /// Removes a specified downvote from the database
        /// </summary>
        /// <param name="vote">The vote to remove</param>
        /// <returns>Successful completion of task</returns>
        public Task DeleteDownVote(DownVote vote);

        /// <summary>
        /// Removes a specified upvote from the database
        /// </summary>
        /// <param name="vote">The vote to remove</param>
        /// <returns>Successful completion of task</returns>
        public Task DeleteUpVote(UpVote vote);

        /// <summary>
        /// Checks to see if the user has already upvoted on a specified post
        /// </summary>
        /// <param name="vote">The vote to check for</param>
        /// <returns>A boolean indicating whether the vote exists within the database</returns>
        public Task<bool> HasUpVoted(UpVote vote);

        /// <summary>
        /// Checks to see if the user has already downvoted on a specified post
        /// </summary>
        /// <param name="vote">The vote to check for</param>
        /// <returns>A boolean indicating whether the vote exists within the database</returns>
        public Task<bool> HasDownVoted(DownVote vote);

        /// <summary>
        /// Retrieves a specified upvote from the database
        /// </summary>
        /// <param name="vote">The specified vote</param>
        /// <returns>The specified vote</returns>
        public Task<UpVote> GetUpVote(UpVote vote);

        /// <summary>
        /// Retrieves a specified downvote from the database
        /// </summary>
        /// <param name="vote">The specified vote</param>
        /// <returns>The specified vote</returns>
        public Task<DownVote> GetDownVote(DownVote vote);
    }
}