using MongoDB.Bson;
using MongoDB.Driver;
using ShowApi.Data.Entities;
using System.Collections.Generic;

namespace ShowApi.Data.Repositories
{
    public class PerformanceRepository : BaseRepository<PerformanceEntity>
    {
        public PerformanceRepository(MongoDBConnection connection) : base(connection)
        {
        }

        public IList<PerformanceEntity> GetByShowId(string id)
        {
            FilterDefinition<PerformanceEntity> filter = Builders<PerformanceEntity>.Filter.Eq("showId", id);
            var feed = dataBase.GetCollection<PerformanceEntity>(Table);
            var result = feed.Find(filter).ToList();
            return result;
        }
    }
}
