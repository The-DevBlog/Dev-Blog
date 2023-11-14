namespace devblog.Models
{
    public class Notification
    {
        public required string UserName { get; set; }
        public int PostId { get; set; }
        public bool Seen { get; set; }
    }
}
