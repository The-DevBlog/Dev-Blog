namespace devblog.Models
{
    public class DownVote
    {
        public int PostId { get; set; }
        public required string UserName { get; set; }
    }
}