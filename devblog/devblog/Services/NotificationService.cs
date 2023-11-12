using devblog.Data;
using devblog.Interfaces;
using devblog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace devblog.Services
{
    public class NotificationService : INotificationService
    {
        public UserManager<User> _userMgr { get; }
        private readonly AppDbContext _db;

        public NotificationService(AppDbContext db, UserManager<User> usermgr)
        {
            _userMgr = usermgr;
            _db = db;
        }

        /// <summary>
        /// Creates a noticication for a new post to every user
        /// </summary>
        public async Task Create(int PostId)
        {
            var allUsers = await _userMgr.Users.ToListAsync();

            allUsers.ForEach(async user =>
            {
                var notification = new Notification
                {
                    UserName = user.UserName,
                    PostId = PostId,
                    Seen = false
                };

                await _db.Notification.AddAsync(notification);
                await _db.SaveChangesAsync();
            });
        }
    }
}
