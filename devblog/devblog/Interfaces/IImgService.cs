namespace devblog.Interfaces
{
    public interface IImgService
    {
        /// <summary>
        /// Uploads an image to Dropbox account
        /// </summary>
        /// <param name="files">Files to upload</param>
        /// <param name="postId">Post Id</param>
        Task Create(IFormFile[] files, int postId);
    }
}
