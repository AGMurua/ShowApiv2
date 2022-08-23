using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ShowApi.Data.Entities
{
    public class ShowEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        public List<PerformanceEntity> PerformanceEntities { get; set; }
        
    }
}
