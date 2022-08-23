using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ShowApi.Data.Entities
{
    public class PerformanceEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public decimal LowestPrice { get; set; }
        public decimal HighestPrice { get; set; }
        public TheaterEntity Teather { get; set; }

    }
}
