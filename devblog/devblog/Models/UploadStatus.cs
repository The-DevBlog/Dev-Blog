namespace devblog.Models
{
    public class UploadStatus
    {
        public HttpResponseMessage? DiscordStatus { get; set; }
        public HttpResponseMessage? MastodonStatus { get; set; }
    }
}
