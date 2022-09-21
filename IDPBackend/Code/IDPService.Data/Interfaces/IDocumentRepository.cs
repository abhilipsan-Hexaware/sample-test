using IDPService.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDPService.Data.Interfaces
{
    public interface IDocumentRepository : IGetAll<Document>, ISave<Document>, IUpdate<Document, string>, IDelete<string>
    {
    }
}
