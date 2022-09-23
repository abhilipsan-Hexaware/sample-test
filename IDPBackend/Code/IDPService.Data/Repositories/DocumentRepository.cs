using IDPService.Data.Interfaces;
using IDPService.Entities.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Bindings;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDPService.Data.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private IGateway _gateway;
        private string _collectionName = "DocumentRepo";

        public DocumentRepository(IGateway gateway)
        {
            _gateway = gateway;
        }
        public IEnumerable<DocumentEntity> GetAll()
        {
            var result = _gateway.GetMongoDB().GetCollection<DocumentEntity>(_collectionName)
                            .Find(new BsonDocument())
                            .ToList();
            return result;
        }

        public bool Save(DocumentEntity entity)
        {
            _gateway.GetMongoDB().GetCollection<DocumentEntity>(_collectionName)
                .InsertOne(entity);
            return true;
        }

        public DocumentEntity Update(string id, DocumentEntity entity)
        {
            var update = Builders<DocumentEntity>.Update
                .Set(e => e.DocId, entity.DocId )
                .Set(e => e.ContentType, entity.ContentType )
                .Set(e => e.FileName, entity.FileName )
                .Set(e => e.CreatedBy, entity.CreatedBy )
                .Set(e => e.UpdatedBy, entity.UpdatedBy )
                .Set(e => e.FilePath, entity.FilePath )
                .Set(e => e.CategorySetId, entity.CategorySetId );

            var result = _gateway.GetMongoDB().GetCollection<DocumentEntity>(_collectionName)
                .FindOneAndUpdate(e => e.Id == id, update);
            return result;
        }

        public bool Delete(string id)
        {
            var result = _gateway.GetMongoDB().GetCollection<DocumentEntity>(_collectionName)
                         .DeleteOne(e => e.Id == id);
            return result.IsAcknowledged;
        }
    }
}
