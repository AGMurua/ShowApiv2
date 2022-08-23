using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ShowApi.Data.Entities
{
    public class RoomEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<SectionEntity> Sections { get; set; }

        public RoomEntity(string id, string name, List<SectionEntity> sections)
        {
            Id = new Guid().ToString();
            Name = name;
            Sections = sections;
        }
    }

    public class SectionEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Seat { get; set; }
    }
}
