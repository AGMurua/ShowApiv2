using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ShowApi.Data.Entities
{
    public class TheaterEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string Description { get; set; }
        public IList<string> Rooms { get; set; }

    }
}
