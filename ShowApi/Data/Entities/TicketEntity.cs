using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ShowApi.Data.Entities
{
    public class TicketEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string TheatherName { get; set; }
        public string Adress { get; set; }
        public string Room { get; set; }
        public string Section { get; set; }
        public IList<string> Seats { get; set; }
    }
}
