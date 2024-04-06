using devblog.Models;

namespace devblog.Interfaces
{
    public interface IUsernameService
    {
        /// <summary>
        /// Creates a new username
        /// </summary>
        /// <param name="userName">new username</param>
        Task Create(string username);

        /// <summary>
        /// Checks whether a username exists
        /// </summary>
        /// <param name="username">username to check</param>
        /// <returns>boolean</returns>
        Task<bool> Exists(string username);
    }
}
