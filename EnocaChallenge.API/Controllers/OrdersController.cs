using EnocaChallenge.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EnocaChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateOrder(int carrierId, int desi)
        {
            try
            {
                
                var order = await _orderService.CreateOrderAsync(carrierId, desi);

               
                return Ok(order);
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }
    }
}