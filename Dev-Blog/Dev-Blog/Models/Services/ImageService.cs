using Azure.Core;
using Dropbox.Api;
using Dropbox.Api.Files;
using ECommerce.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ECommerce.Models.Services
{
    public class ImageService : IImage
    {
        private readonly IConfiguration _config;

        public ImageService(IConfiguration configuration)
        {
            _config = configuration;
        }

        /// <summary>
        /// Uploads an image to a dropbox account
        /// </summary>
        /// <param name="image">The image to upload</param>
        /// <param name="imgName">The desired name of the image</param>
        /// <returns>Successful completion of task</returns>
        public async Task<string> Upload(IFormFile image, string imgName)
        {
            var url = "";
            var destination = _config["DestinationPath"] + imgName;

            // TODO: add functionality to add an image from anywhere in my local directory.
            using (var dbx = new DropboxClient(_config["DropboxToken"]))
            {
                string srcFile = _config["PathToImgs"] + image.FileName;
                using (var fs = new FileStream(srcFile, FileMode.Open))
                {
                    var updated = await dbx.Files.UploadAsync(
                        destination,
                        WriteMode.Overwrite.Instance,
                        body: fs
                    );
                };

                // create shareable link
                var link = dbx.Sharing.CreateSharedLinkWithSettingsAsync(destination);
                link.Wait();

                url = link.Result.Url;

                // remove id and replace with raw=1
                url = url.Substring(0, url.Length - 4) + "raw=1";
            }
            return url;
        }
    }
}