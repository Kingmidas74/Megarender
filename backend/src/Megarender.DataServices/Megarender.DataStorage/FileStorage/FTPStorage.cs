using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Megarender.DataStorage
{
    public class FTPStorage : IFileStorage
    {

        public FTPStorage(string rootPath)
        {
            
        }

        public Task<BlobContainerClient> CreateDirectoryAsync(string directoryName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<BlobDownloadInfo> GetFileAsync(string directory, string filename, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<BlobContentInfo> UploadFileAsync(string directory, string filename, byte[] content, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }

}