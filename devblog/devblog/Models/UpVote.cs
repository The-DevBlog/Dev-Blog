namespace devblog.Models
{
    public class UpVote
    {
        public int PostId { get; set; }
        public required string UserName { get; set; }
    }
}