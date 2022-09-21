using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IDPService.Entities.Entities
{
    [BsonIgnoreExtraElements]
    public class Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id  { get; set; }
        public string DocId  { get; set; }
        public string ContentType  { get; set; }
        public string FileName  { get; set; }
        public string CreatedBy  { get; set; }
        public string UpdatedBy  { get; set; }
        public string FilePath  { get; set; }
        public string CategorySetId  { get; set; }
        
    }

}
