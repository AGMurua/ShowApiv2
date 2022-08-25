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
        public string ShowName { get; set; }
        public string ShowId { get; set; }
        public string TeatherName { get; set; }
        public string Adress { get; set; }
        public string RoomName { get; set; }
        public DateTime Date { get; set; }
        public decimal LowestPrice { get; set; }
        public decimal HighestPrice { get; set; }
        public IList<SectionByPrice> Sections { get; set; }

    }
    public class SectionByPrice
    {
        public string SectionId { get; set; }
        public string SectionName { get; set; }
        public IList<string> Seats { get; set; }
        public IList<string> SoldSeats { get; set; }
        public decimal Price { get; set; } 
    }
}
