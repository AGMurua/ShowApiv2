using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ShowApi.Data.Entities
{
    public class PerformanceEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string ShowId { get; set; }
        public string TeatherId { get; set; }
        public string RoomId { get; set; }
        public DateTime Date { get; set; }
        public decimal LowestPrice { get; set; }
        public decimal HighestPrice { get; set; }
        public IList<SectionByPrice> Sections { get; set; }

    }
    public class SectionByPrice
    {
        public string SectionId { get; set; }
        public decimal Price { get; set; } 
    }
}
