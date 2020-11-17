namespace Dev_Blog.Models.ViewModels
{
    public class CommentVM
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public string Date { get; set; }
    }
}