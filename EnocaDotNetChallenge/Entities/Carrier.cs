using System.Collections.Generic;

namespace EnocaChallenge.Core.Entities
{
    public class Carrier : BaseEntity
    {
        public int CarrierId { get; set; } 
        public string CarrierName { get; set; } 
        public bool CarrierIsActive { get; set; } 
        public int CarrierPlusDesiCost { get; set; } 
        public int CarrierConfigurationId { get; set; } 

        public ICollection<Order> Orders { get; set; }
        public ICollection<CarrierConfiguration> CarrierConfigurations { get; set; }
    }
}