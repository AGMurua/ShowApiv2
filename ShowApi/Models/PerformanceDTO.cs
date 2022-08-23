using System;

namespace ShowApi.Models
{
    public class PerformanceDTO : PerformanceCrudDTO
    {
        public string Id { get; set; }
        public decimal LowestPrice { get; set; }
        public decimal HighestPrice { get; set; }
    }
    public class PerformanceCrudDTO
    {
        public string ShowId { get; set; }
        public string TeatherId { get; set; }
        public string RoomId { get; set; }
        public DateTime Date { get; set; }
    }
}
