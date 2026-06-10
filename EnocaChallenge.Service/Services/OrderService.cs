using EnocaChallenge.Core.Entities;
using EnocaChallenge.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<Order> CreateOrderAsync(int desi)
        {
            
            var suitableConfigs = await _context.CarrierConfigurations
                .Include(c => c.Carrier)
                .Where(c => c.CarrierMinDesi <= desi && c.CarrierMaxDesi >= desi)
                .ToListAsync();

            CarrierConfiguration selectedConfig = null;
            decimal finalCost = 0;

            if (suitableConfigs.Any())
            {
                
                selectedConfig = suitableConfigs.OrderBy(c => c.CarrierCost).First();
                finalCost = selectedConfig.CarrierCost;
            }
            else
            {
               
                var allConfigs = await _context.CarrierConfigurations
                    .Include(c => c.Carrier)
                    .ToListAsync();

                if (!allConfigs.Any())
                    throw new Exception("Sistemde kayıtlı kargo konfigürasyonu bulunamadı.");

               
                selectedConfig = allConfigs.OrderBy(c => Math.Abs(desi - c.CarrierMaxDesi)).First();

                
                int desiFarki = desi - selectedConfig.CarrierMaxDesi;

               
                desiFarki = desiFarki > 0 ? desiFarki : 0;

                finalCost = selectedConfig.CarrierCost + (desiFarki * selectedConfig.Carrier.CarrierPlusDesiCost);
            }

            
            var order = new Order
            {
                CarrierId = selectedConfig.CarrierId,
                OrderDesi = desi,
                OrderDate = DateTime.Now,
                OrderCarrierCost = finalCost
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }
    }
}