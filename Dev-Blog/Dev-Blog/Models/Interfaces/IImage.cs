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
        /// Gets a specified container from Azure storage
        /// </summary>
        /// <param name="name">Name of container to get</param>
        /// <returns>container</returns>
        public Task<CloudBlobContainer> GetContainer(string name);

        /// <summary>
        /// Gets a specified BLOB from an azure container
        /// </summary>
        /// <param name="imageName">Name of specified image</param>
        /// <param name="containerName">Name of specified container</param>
        /// <returns>BLOB</returns>
        public Task<CloudBlob> GetBlob(string imageName, string containerName);

        /// <summary>
        /// Uploads a BLOB to specified container in Azure storage
        /// </summary>
        /// <param name="containerName">Name of specified container</param>
        /// <param name="fileName">Name of specified file</param>
        /// <param name="image">Name of specified image</param>
        /// <param name="contentType">Type of content being uploaded</param>
        /// <returns>Successful completion of task</returns>
        public Task UploadFile(string containerName, string fileName, byte[] image, string contentType);
    }
}