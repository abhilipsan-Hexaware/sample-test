using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using IDPService.Data.Interfaces;
using IDPService.Entities.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace IDPService.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
  private IGateway _gateway;
        private string _collectionName = "Category";

        public CategoryRepository(IGateway gateway)
        {
            _gateway = gateway;
        }
        public string Save(Category entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
                entity.Id = ObjectId.GenerateNewId().ToString();

            _gateway.GetMongoDB().GetCollection<Category>(_collectionName)
                .ReplaceOne(doc => doc.Id == entity.Id, entity,
                new ReplaceOptions { IsUpsert = true });

            return entity.Id;
        }
        public Category GetBuiltInCategoryByName(string name)
        {
            var result = _gateway.GetMongoDB().GetCollection<Category>(_collectionName)
                           .Find(o => o.Name == name)
                           .FirstOrDefault();
            return result;
        }
    }
}