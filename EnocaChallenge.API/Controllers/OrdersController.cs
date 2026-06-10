using EnocaChallenge.Data.Contexts;
using EnocaChallenge.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnocaChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly EnocaDbContext _context;

        
        public OrdersController(OrderService orderService, EnocaDbContext context)
        {
            _orderService = orderService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(int desi)
        {
            try
            {
                
                var order = await _orderService.CreateOrderAsync(desi);

               
                return Ok("Sipariş eklendi.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            return Ok(await _context.Orders.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound("Sipariş bulunamadı.");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

           
            return Ok($"{id} ID'li sipariş silindi.");
        }
    }
}