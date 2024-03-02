namespace devblog.Models
{
    public class Notification
    {
        public int PostId { get; set; }
        public required string NotificationType { get; set; }
        public bool Seen { get; set; }
        public required string UserName { get; set; }
    }
}
