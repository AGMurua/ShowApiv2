using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ShowApi.Data.Entities
{
    public class RoomEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

        public string Id { get; set; }
        public string TheaterId { get; set; }
        public string Name { get; set; }
        public IList<string> Sections { get; set; }
    }

    public class SectionEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

        public string Id { get; set; }
        public string RoomId { get; set; }
        public string Name { get; set; }
        public IList<string> Seat { get; set; }
    }
}
