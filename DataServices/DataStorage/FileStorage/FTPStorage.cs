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

        public Task<BlobContainerClient> CreateDirectory(string directoryName)
        {
            throw new System.NotImplementedException();
        }

        public Task<BlobDownloadInfo> GetFile(string directory, string filename)
        {
            throw new System.NotImplementedException();
        }

        public Task<BlobContentInfo> UploadFile(string directory, string filename, byte[] content)
        {
            throw new System.NotImplementedException();
        }
    }

}