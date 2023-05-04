namespace devblog.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public DateTime Date { get; set; }

        public List<Img>? Imgs { get; set; }

        public List<UpVote>? UpVotes { get; set; }

        public List<DownVote>? DownVotes { get; set; }

        public List<Comment>? Comments { get; set; }
    }
}