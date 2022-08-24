using ShowApi.Data.Entities;
using System.Collections.Generic;

namespace ShowApi.Models
{
    public class RoomDTO
    {
        public string Id { get; set; }
        public string TheaterId { get; set; }
        public string Name { get; set; }
        public IList<string> Sections { get; set; }

    }


    public class SectionDTO
    {
        public string Id { get; set; }
        public string RoomId { get; set; }
        public string Name { get; set; }
        public IList<string> Seat { get; set; }

    }
}

