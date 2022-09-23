using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace IDPService.Entities.Entities
{
    public class UploadFile
    {
        public IList<IFormFile> Files { get; set; }

        public string CreatedBy {get;set;}

        public string CategorySetId {get; set; }
    }
}
