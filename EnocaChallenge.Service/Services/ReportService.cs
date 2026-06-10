using EnocaChallenge.Core.Entities;
using EnocaChallenge.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EnocaChallenge.Service.Services
{
    public class ReportService
    {
        private readonly EnocaDbContext _context;

        public ReportService(EnocaDbContext context)
        {
            _context = context;
        }

        public async Task GenerateHourlyCarrierReportsAsync()
        {
           
            var groupedOrders = await _context.Orders
                .GroupBy(o => new { o.CarrierId, o.OrderDate.Date })
                .Select(g => new
                {
                    CarrierId = g.Key.CarrierId,
                    ReportDate = g.Key.Date,
                    TotalCost = g.Sum(x => x.OrderCarrierCost)
                })
                .ToListAsync();

           
            foreach (var item in groupedOrders)
            {
                
                var existingReport = await _context.CarrierReports
                    .FirstOrDefaultAsync(r => r.CarrierId == item.CarrierId && r.CarrierReportDate == item.ReportDate);

                if (existingReport != null)
                {
                   
                    existingReport.CarrierCost = item.TotalCost;
                }
                else
                {
                    
                    _context.CarrierReports.Add(new CarrierReport
                    {
                        CarrierId = item.CarrierId,
                        CarrierReportDate = item.ReportDate,
                        CarrierCost = item.TotalCost
                    });
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}