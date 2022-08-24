using System.Collections.Generic;

namespace ShowApi.Models
{
    public class ReserveCrudDto
    {
        public string ShowId { get; set; }
        public string PerformanceId { get; set; }
        public string SectionId { get; set; }
        public IList<string> Seats { get; set; }
    }
    public class TicketDto : ReserveDto
    {
        public decimal TotalPrice { get; set; }

    }
    public class ReserveDto : ReserveCrudDto
    {
    }
}
