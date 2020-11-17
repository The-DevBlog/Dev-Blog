namespace Dev_Blog.Models
{
    public class Vote
    {
        public int PostId { get; set; }
        public string UserId { get; set; }
        public bool UpVote { get; set; }
        public bool DownVote { get; set; }

        public Post Post { get; set; }
    }
}