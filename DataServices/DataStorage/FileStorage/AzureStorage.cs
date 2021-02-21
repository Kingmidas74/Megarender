
using System.IO;
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

        public async Task<BlobContainerClient> CreateDirectory(string directoryName)
        {
            return await _blobServiceClient.CreateBlobContainerAsync(directoryName, Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
        }

        public async Task<BlobDownloadInfo> GetFile(string directory, string filename)
        {
            var container = _blobServiceClient.GetBlobContainerClient(directory);
            var blob = container.GetBlobClient(filename);
            return (await blob.DownloadAsync()).Value;
        }

        public async Task<BlobContentInfo> UploadFile(string directory, string filename, byte[] content)
        {
            var container = _blobServiceClient.GetBlobContainerClient(directory);
            var blob = await container.UploadBlobAsync(filename, new MemoryStream(content));
            return blob.Value;
        }
    }
}