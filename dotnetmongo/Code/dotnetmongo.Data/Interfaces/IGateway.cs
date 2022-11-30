using MongoDB.Driver;

namespace dotnetmongo.Data.Interfaces
{
    public interface IGateway
    {
        IMongoDatabase GetMongoDB();
    }
}
