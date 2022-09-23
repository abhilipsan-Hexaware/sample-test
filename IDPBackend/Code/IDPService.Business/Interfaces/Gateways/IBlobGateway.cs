using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IDPService.Business.Interfaces.Gateways
{
    public interface IBlobGateway
    {
        Task<string> UploadFileAsyn(IFormFile file, string uploadFileName, string containerName, string folderName = null);
    }
}
