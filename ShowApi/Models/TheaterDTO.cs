using ShowApi.Data.Entities;
using System.Collections.Generic;

namespace ShowApi.Models
{
    public class TheaterDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string Description { get; set; }
        public IList<string> Rooms { get; set; }

    }
}
