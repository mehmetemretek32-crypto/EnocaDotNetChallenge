using System;

namespace EnocaChallenge.Core.Entities
{
    public class CarrierReport
    {
        public int CarrierReportId { get; set; }
        public int CarrierId { get; set; }
        public decimal CarrierCost { get; set; }
        public DateTime CarrierReportDate { get; set; }
    }
}