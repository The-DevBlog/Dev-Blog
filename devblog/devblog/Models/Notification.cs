namespace devblog.Models
{
    public class Notification
    {
        public int PostId { get; set; }
        public NotificationType NotificationType { get; set; }
        public bool Seen { get; set; }
        public required string UserName { get; set; }
    }

    public enum NotificationType
    {
        PostNew,
        CommentNew,
        CommentReply
    }
}
