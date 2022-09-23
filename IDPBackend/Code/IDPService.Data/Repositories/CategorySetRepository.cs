using System;
using System.Collections.Generic;
using IDPService.Data.Interfaces;
using IDPService.Entities.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace IDPService.Data.Repositories
{
    public class CategorySetRepository : ICategorySetRepository
    {
        private readonly IGateway _gateway;
        private readonly string _collectionName = "CategorySet";
        private readonly string _docId = "_id";


        public CategorySetRepository(IGateway gateway)
        {
            _gateway = gateway;
        }

        public CategorySet GetById(string id)
        {
            var result = _gateway.GetMongoDB().GetCollection<CategorySet>(_collectionName)
                           .Find(o => o.Id == id)
                           .FirstOrDefault();
            return result;
        }

        public string SaveBuiltInCategorySet(CategorySet entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
                entity.Id = ObjectId.GenerateNewId().ToString();

            _gateway.GetMongoDB().GetCollection<CategorySet>(_collectionName)
                .ReplaceOne(doc => doc.Id == entity.Id, entity,
                new ReplaceOptions { IsUpsert = true });

            return entity.Id;
        }

        public IEnumerable<CategorySet> GetAll()
        {
            var result = _gateway.GetMongoDB().GetCollection<CategorySet>(_collectionName)
                            .Find(c => String.IsNullOrEmpty(c.SubscriptionId))
                            .ToList();
            return result;
        }


        public CategorySet GetBuiltInCategorySetByName(string name)
        {
            var result = _gateway.GetMongoDB().GetCollection<CategorySet>(_collectionName)
                           .Find(o => o.Name == name)
                           .FirstOrDefault();
            return result;
        }

        public CategorySet GetByName(string name)
        {
            var result = _gateway.GetMongoDB().GetCollection<CategorySet>(_collectionName)
                           .Find(o => o.Name == name)
                           .FirstOrDefault();
            return result;
        }

        public bool Delete(string categoryId)
        {
            var filter = Builders<CategorySet>.Filter.Eq(_docId, ObjectId.Parse(categoryId));

            var collection = _gateway.GetMongoDB().GetCollection<CategorySet>(_collectionName);

            return collection.DeleteOne(filter).IsAcknowledged;
        }

        public bool UpdateComposedModelId(string categorySetId, string composedModelId)
        {
            var update = Builders<CategorySet>.Update.Set(a => a.ComposedModelId, composedModelId);

            _gateway.GetMongoDB().GetCollection<CategorySet>(_collectionName)
                .UpdateOne(a => a.Id == categorySetId, update);

            return true;
        }
    }
}
