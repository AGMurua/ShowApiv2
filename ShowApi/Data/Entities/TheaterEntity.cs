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
        public IList<RoomEntity> Rooms { get; set; }

    }
}
