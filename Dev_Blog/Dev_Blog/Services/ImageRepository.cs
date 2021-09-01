using Dev_Blog.Interfaces;
using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Dev_Blog.Services
{
    public class ImageRepository : IImageRepository
    {
        private IConfiguration _config;

        public ImageRepository(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Uploads an image to Dropbox account
        /// </summary>
        /// <param name="fs">Image file stream</param>
        /// <param name="name">Name of image</param>
        /// <returns>Dropbox url to image</returns>
        public async Task<string> AddImgToDropBox(Stream fs, string name)
        {
            // create unique file name
            string ext = Path.GetExtension(name);
            string fileName = $"{DateTime.Now.Ticks}{name}{ext}";

            string url = "";
            string dest = _config["DestinationPath"] + fileName;

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