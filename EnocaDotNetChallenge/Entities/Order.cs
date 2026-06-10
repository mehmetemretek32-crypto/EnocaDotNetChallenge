using System;

namespace EnocaChallenge.Core.Entities
{
    public class Order : BaseEntity
    {
        public int OrderId { get; set; } 
        public int CarrierId { get; set; } 
        public int OrderDesi { get; set; } 
        public DateTime OrderDate { get; set; } 
        public decimal OrderCarrierCost { get; set; } 

        
        public Carrier Carrier { get; set; }
    }
}