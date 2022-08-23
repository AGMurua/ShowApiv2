using System.Collections.Generic;

namespace ShowApi.Models
{
    public class ShowDTO : CrudShowDTO
    {
        public string Id { get; set; }

    }

    public class CrudShowDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public string MPARating { get; set; }
        public IList<string> Cast { get; set; }
    }
}
