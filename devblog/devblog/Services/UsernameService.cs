using devblog.Data;
using devblog.Interfaces;
using devblog.Models;

namespace devblog.Services
{
    public class UsernameService : IUsernameService
    {
        private readonly UserDbContext _db;

        public UsernameService(UserDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Creates a new username
        /// </summary>
        /// <param name="userName">new username</param>
        public async Task Create(string username)
        {
            Username user = new Username(username);

            _db.usernames.Add(user);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Checks whether a username exists
        /// </summary>
        /// <param name="username">username to check</param>
        /// <returns>boolean</returns>
        public async Task<bool> Exists(string username)
        {
            Username user = _db.usernames.Where(x => x.Name == username).FirstOrDefault();
            return user != null;
        }
    }
}