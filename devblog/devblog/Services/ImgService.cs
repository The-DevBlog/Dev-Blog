using devblog.Data;
using devblog.Interfaces;
using devblog.Models;
using Dropbox.Api;
using Dropbox.Api.Files;

namespace devblog.Services
{
    public class ImgService : IImgService
    {
        private readonly IConfiguration _config;
        private AppDbContext _db;


        public ImgService(IConfiguration config, AppDbContext db)
        {
            _config = config;
            _db = db;
        }

        /// <summary>
        /// Uploads an image to Dropbox account
        /// </summary>
        /// <param name="files">Files to upload</param>
        /// <param name="postId">Post Id</param>
        public async Task<HttpResponseMessage> Create(IFormFile[] files, int postId)
        {
            var res = new HttpResponseMessage();
            foreach (var f in files)
            {
                try
                {
                    var stream = f.OpenReadStream();
                    string url = await AddImgToDropBox(stream, f.FileName);

                    Img img = new Img
                    {
                        PostId = postId,
                        Url = url,
                    };

                    _db.Img.Add(img);
                    await _db.SaveChangesAsync();
                } catch (Exception e)
                {
                    res.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    res.ReasonPhrase = $"Failed to upload file to DropBox: {e.Message}";
                    return res;
                }
            }

            res.StatusCode = System.Net.HttpStatusCode.OK;
            return res;
        }
        /// <summary>
        /// Delete an img from dropbox account
        /// </summary>
        /// <param name="imgs">List of imgs to delete</param>
        public async Task DeleteImgFromDropBox(List<Img> imgs)
        {
            using (var dbx = new DropboxClient(_config["DropboxToken"]))
            {
                imgs.ForEach(img =>
                {
                    try
                    {
                        // exract the filename from the img url
                        Uri uri = new Uri(img.Url);
                        var name = uri.Segments[uri.Segments.Length - 1];

                        // delete file from dropbox
                        var result = dbx.Files.DeleteV2Async($"{_config["DropboxDestinationPath"]}{name}").Result;

                    }
                    catch (Exception e)
                    {
                        throw new Exception("Error getting dropbox file: ", e);
                    }
                });
            }
        }

        public async Task<string> AddImgToDropBox(Stream fs, string name)
        {
            // create unique file name
            string fileName = $"{DateTime.Now.Ticks}{name}";
            var destinationPath = _config["DropboxDestinationPath"];
            string url = "";
            string dest = destinationPath + fileName;

            using (var dbx = new DropboxClient(_config["DropboxToken"]))
            {
                // upload file to dbx
                var updated = await dbx.Files.UploadAsync(
                    dest,
                    WriteMode.Overwrite.Instance,
                    body: fs
                );
                fs.Close();

                // create shareable link
                var link = dbx.Sharing.CreateSharedLinkWithSettingsAsync(dest);
                link.Wait();

                url = link.Result.Url;

                // remove id and replace with raw=1
                url = url.Substring(0, url.Length - 4) + "raw=1";
            }

            return url;
        }
    }
}
