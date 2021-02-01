namespace DevBlog_BlazorServer.Models
{
    public class VoteModel
    {
        public int PostId { get; set; }
        public string UserId { get; set; }
        public bool UpVote { get; set; }
        public bool DownVote { get; set; }
        public PostModel Post { get; set; }
    }
}