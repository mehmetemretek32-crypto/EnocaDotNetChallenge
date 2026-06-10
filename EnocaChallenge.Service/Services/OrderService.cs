using EnocaChallenge.Core.Entities;
using EnocaChallenge.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EnocaChallenge.Service.Services
{
    public class OrderService
    {
        private readonly EnocaDbContext _context;

        public OrderService(EnocaDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(int carrierId, int orderDesi)
        {
            
            var carrier = await _context.Carriers.FindAsync(carrierId);
            if (carrier == null)
                throw new System.Exception("Böyle bir kargo firması bulunamadı!");

            
            var configuration = await _context.CarrierConfigurations
                .Where(c => c.CarrierId == carrierId && c.CarrierMinDesi <= orderDesi && c.CarrierMaxDesi >= orderDesi)
                .FirstOrDefaultAsync();

            decimal calculatedCost = 0;

            if (configuration != null)
            {
               
                calculatedCost = configuration.CarrierCost;
            }
            else
            {
               
                var maxConfig = await _context.CarrierConfigurations
                    .Where(c => c.CarrierId == carrierId)
                    .OrderByDescending(c => c.CarrierMaxDesi)
                    .FirstOrDefaultAsync();

                if (maxConfig != null)
                {
                    int extraDesi = orderDesi - maxConfig.CarrierMaxDesi;
                    calculatedCost = maxConfig.CarrierCost + (extraDesi * carrier.CarrierPlusDesiCost);
                }
            }

            
            var newOrder = new Order
            {
                CarrierId = carrierId,
                OrderDesi = orderDesi,
                OrderDate = System.DateTime.Now,
                OrderCarrierCost = calculatedCost
            };

            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();

            return newOrder;
        }
    }
}