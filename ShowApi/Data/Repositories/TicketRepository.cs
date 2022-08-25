using MongoDB.Driver;
using ShowApi.Data.Entities;
using System.Collections.Generic;

namespace ShowApi.Data.Repositories
{
    public class TicketRepository : BaseRepository<TicketEntity>
    {
        public TicketRepository(MongoDBConnection connection) : base(connection)
        {
        }

        public IList<TicketEntity> GetByUserId(string id)
        {
            FilterDefinition<TicketEntity> filter = Builders<TicketEntity>.Filter.Eq("UserId", id);
            var feed = dataBase.GetCollection<TicketEntity>(Table);
            var result = feed.Find(filter).ToList();
            return result;
        }
    }
}
