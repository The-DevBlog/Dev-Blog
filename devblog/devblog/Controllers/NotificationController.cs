using devblog.Interfaces;
using devblog.Services;
using Microsoft.AspNetCore.Mvc;

namespace devblog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notifications;

        public NotificationController(NotificationService notifications)
        {
            _notifications = notifications;
        }

        public async Task Get()
        {

        }
    }
}
