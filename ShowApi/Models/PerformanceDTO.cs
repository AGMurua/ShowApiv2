using System;
using System.Collections.Generic;

namespace ShowApi.Models
{

    public class PerformanceCrudDTO
    {
        public string ShowId { get; set; }
        public string TeatherId { get; set; }
        public string RoomId { get; set; }
        public DateTime Date { get; set; }
        public IList<PerformanceSectionPriceDTO> Sections { get; set; }
    }
    public class PerformanceSectionPriceDTO
    {
        public string SectionId { get; set; }
        public decimal? Price { get; set; }
    }

    public class PerformanceDTO
    {
        public string Id { get; set; }
        public string ShowName { get; set; }
        public string ShowId { get; set; }
        public string TeatherName { get; set; }
        public string Adress { get; set; }
        public string RoomName { get; set; }
        public DateTime Date { get; set; }
        public decimal LowestPrice { get; set; }
        public decimal HighestPrice { get; set; }
        public IList<PerformanceSectionDTO> Sections { get; set; }

    }
    public class PerformanceSectionDTO
    {
        public string SectionId { get; set; }
        public string SectionName { get; set; }
        public IList<string> Seats { get; set; }
        public IList<string> SoldSeats { get; set; }
        public decimal Price { get; set; }
    }
}

