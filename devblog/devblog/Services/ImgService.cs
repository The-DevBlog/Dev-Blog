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
        public async Task Create(IFormFile[] files, int postId)
        {
            foreach (var f in files)
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
            }
        }
        // "https://www.dropbox.com/scl/fi/0x5xo53l9rkg41bss41uk/638257148450375126a.png?rlkey=xuarjst596rg4t1jk38dvqhws&raw=1"
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
