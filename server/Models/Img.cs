namespace devblog.Models
{
    public class Img
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public required string Url { get; set; }
    }
}