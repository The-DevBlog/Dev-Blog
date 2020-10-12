using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models.Interfaces
{
    public interface IImage
    {
        /// <summary>
        /// Uploads an image to a dropbox account
        /// </summary>
        /// <param name="image">The image to upload</param>
        /// <param name="imgName">The desired name of the image</param>
        /// <returns>Successful completion of task</returns>
        public Task<string> Upload(IFormFile image, string imgName);
    }
}