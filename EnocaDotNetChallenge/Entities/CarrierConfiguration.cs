namespace EnocaChallenge.Core.Entities
{
    public class CarrierConfiguration : BaseEntity
    {
        public int CarrierConfigurationId { get; set; } 
        public int CarrierId { get; set; } 
        public int CarrierMaxDesi { get; set; } 
        public int CarrierMinDesi { get; set; } 
        public decimal CarrierCost { get; set; } 

       
        public Carrier Carrier { get; set; }
    }
}