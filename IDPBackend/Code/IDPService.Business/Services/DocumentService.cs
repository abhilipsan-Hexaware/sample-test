using IDPService.Business.Interfaces;
using IDPService.Data.Interfaces;
using IDPService.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDPService.Business.Services
{
    public class DocumentService : IDocumentService
    {
        IDocumentRepository _DocumentRepository;

        public DocumentService(IDocumentRepository DocumentRepository)
        {
           this._DocumentRepository = DocumentRepository;
        }
        public IEnumerable<Document> GetAll()
        {
            return _DocumentRepository.GetAll();
        }

        public Document Save(Document Document)
        {
            _DocumentRepository.Save(Document);
            return Document;
        }

        public Document Update(string id, Document Document)
        {
            return _DocumentRepository.Update(id, Document);
        }

        public bool Delete(string id)
        {
            return _DocumentRepository.Delete(id);
        }

    }
}
