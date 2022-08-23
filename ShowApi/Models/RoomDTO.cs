using ShowApi.Data.Entities;
using System.Collections.Generic;

namespace ShowApi.Models
{
    public class RoomDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<SectionDTO> Sections { get; set; }

    }


    public class SectionDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Seat { get; set; }

    }
}

