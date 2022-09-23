using IDPService.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDPService.Data.Interfaces
{
    public interface IDocumentRepository : IGetAll<DocumentEntity>, ISave<DocumentEntity>, IUpdate<DocumentEntity, string>, IDelete<string>
    {
    }
}
