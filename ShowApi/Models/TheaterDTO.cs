using ShowApi.Data.Entities;
using System.Collections.Generic;

namespace ShowApi.Models
{
    public class TheaterDTO : TheaterCrudDTO
    {
        public string Id { get; set; }


    }
    public class TheaterCrudDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string Description { get; set; }
        public IList<string> Rooms { get; set; }
    }
}
