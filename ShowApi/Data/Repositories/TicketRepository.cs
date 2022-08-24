using ShowApi.Data.Entities;

namespace ShowApi.Data.Repositories
{
    public class TicketRepository : BaseRepository<TicketEntity>
    {
        public TicketRepository(MongoDBConnection connection) : base(connection)
        {
        }
    }
}
