using System.IO;
using System.Threading.Tasks;
using IDPService.Business.Interfaces.Gateways;
using Microsoft.AspNetCore.Http;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace Dms.Business.Gateway
{
    public class BlobGateway : IBlobGateway
    {
        private readonly IConfiguration _configuration;
        public BlobGateway(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> UploadFileAsyn(IFormFile file, string uploadFileName, string containerName, string folderName = null)
        {
            BlobContainerClient container = new BlobContainerClient(_configuration["Blob:AzureStorageConnectionString"], containerName);
            // container.Create();
            BlobClient blob = container.GetBlobClient(uploadFileName + Path.GetExtension(file.FileName));
            await blob.UploadAsync(file.OpenReadStream());
            return blob.Uri.AbsolutePath;
        }

    }
}
