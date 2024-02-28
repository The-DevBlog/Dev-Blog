using devblog.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace devblog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notifications;
        private readonly IImgService _imgs;

        public NotificationsController(INotificationService notifications, IImgService imgs)
        {
            _notifications = notifications;
            _imgs = imgs;
        }

        /// <summary>
        /// Gets all notifications for a specific user
        /// </summary>
        /// <param name="userName">Username to retrive unseen notifications for</param>
        /// <returns>List<Notification></returns>
        [Authorize]
        [HttpGet("{userName}")]
        public async Task<List<NotificationViewModel>> Get(string userName)
        {
            var notificationsVM = new List<NotificationViewModel>();
            var notifications = await _notifications.Get(userName);

            foreach (var item in notifications)
            {
                var img = await _imgs.Get(item.PostId);
                notificationsVM.Add(new NotificationViewModel
                {
                    UserName = item.UserName,
                    PostId = item.PostId,
                    ImgUrl = img.Url
                });
            }

            return notificationsVM.OrderByDescending(n => n.PostId).ToList();
        }

        /// <summary>
        /// Delete a specified notification
        /// </summary>
        [Authorize]
        [HttpDelete("{postId}/{userName}")]
        public async Task Delete(int postId, string userName)
        {
            await _notifications.Delete(postId, userName);
        }
    }

    public class NotificationViewModel
    {
        public string UserName { get; set; }
        public int PostId { get; set; }
        public string ImgUrl { get; set; }
    }
}
