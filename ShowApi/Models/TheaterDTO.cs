using ShowApi.Data.Entities;
using System.Collections.Generic;

namespace ShowApi.Models
{
    public class TheaterDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<RoomDTO> Rooms { get; set; }

    }
}
