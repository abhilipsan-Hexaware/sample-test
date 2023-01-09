using MongoDB.Driver;

namespace dotnetpipeline.Data.Interfaces
{
    public interface IGateway
    {
        IMongoDatabase GetMongoDB();
    }
}
