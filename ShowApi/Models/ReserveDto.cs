using System;
using System.Collections.Generic;

namespace ShowApi.Models
{
    public class ReserveCrudDto
    {
        public string PerformanceId { get; set; }
        public string SectionId { get; set; }
        public IList<string> Seats { get; set; }
    }
    public class ReserveDto : ReserveCrudDto
    {
        public decimal TotalPrice { get; set; }

    }
    public class TicketDTO
    {
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
