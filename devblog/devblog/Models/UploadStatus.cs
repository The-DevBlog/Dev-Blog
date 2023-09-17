namespace devblog.Models
{
    public class UploadStatus
    {
        public required HttpResponseMessage DiscordStatus { get; set; }
        public required HttpResponseMessage MastodonStatus { get; set; }
    }
}
