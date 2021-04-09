
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Megarender.DataStorage
{
    public class AzureStorage : IFileStorage
    {
        private readonly BlobServiceClient _blobServiceClient;
        public AzureStorage(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<BlobContainerClient> CreateDirectoryAsync(string directoryName, CancellationToken cancellationToken = default)
        {
            return await _blobServiceClient.CreateBlobContainerAsync(directoryName, Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
        }

        public async Task<BlobDownloadInfo> GetFileAsync(string directory, string filename, CancellationToken cancellationToken = default)
        {
            var container = _blobServiceClient.GetBlobContainerClient(directory);
            var blob = container.GetBlobClient(filename);
            return (await blob.DownloadAsync()).Value;
        }

        public async Task<BlobContentInfo> UploadFileAsync(string directory, string filename, byte[] content, CancellationToken cancellationToken = default)
        {
            var container = _blobServiceClient.GetBlobContainerClient(directory);
            var blob = await container.UploadBlobAsync(filename, new MemoryStream(content), cancellationToken);
            return blob.Value;
        }
    }
}