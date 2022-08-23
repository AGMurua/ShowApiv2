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
        public string Name { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public string MPARating { get; set; }
        public IList<string> Cast { get; set; }

    }
}
