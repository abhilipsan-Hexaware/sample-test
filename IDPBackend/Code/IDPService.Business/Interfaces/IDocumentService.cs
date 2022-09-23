using IDPService.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace IDPService.Business.Interfaces
{
    public interface IDocumentService
    {
        IEnumerable<DocumentEntity> GetAll();
        DocumentEntity Save(DocumentEntity classification);
        DocumentEntity Update(string id, DocumentEntity classification);
        bool Delete(string id);

        bool UploadInvoiceFiles(List<IFormFile> files, string createdBy, string pageRange, string batchId = null);


    }
}
