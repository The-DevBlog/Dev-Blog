using devblog.Models;
using Microsoft.AspNetCore.Identity;

namespace devblog.Interfaces
{
    public interface IAccountService
    {
        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user">New user to add</param>
        /// <returns>Task<IdentityResult></returns>
        Task<IdentityResult> Create(User user);
    }
}
