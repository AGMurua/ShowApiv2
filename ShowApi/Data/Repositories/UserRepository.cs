using MongoDB.Driver;
using ShowApi.Data.Entities;

namespace ShowApi.Data.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>
    {
        public UserRepository(MongoDBConnection connection) : base(connection)
        {
        }

        public UserEntity FindByUserName(string username)
        {
            FilterDefinition<UserEntity> filter = Builders<UserEntity>.Filter.Eq("UserName", username);
            var feed = dataBase.GetCollection<UserEntity>(Table);
            var result = feed.Find(filter).FirstOrDefault();
            return result;
        }
    }
}
