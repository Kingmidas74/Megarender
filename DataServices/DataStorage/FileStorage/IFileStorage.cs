
using System;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Megarender.DataStorage
{
    public interface IFileStorage
    {
        public Task<BlobContainerClient> CreateDirectory(string directoryName);

        public Task<BlobDownloadInfo> GetFile(string directory, string filename);

        public Task<BlobContentInfo> UploadFile(string directory, string filename, byte[] content);
    }
}