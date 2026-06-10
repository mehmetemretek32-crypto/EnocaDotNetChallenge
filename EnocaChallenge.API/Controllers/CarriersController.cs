using EnocaChallenge.Core.Entities;
using EnocaChallenge.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnocaChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarriersController : ControllerBase
    {
        private readonly EnocaDbContext _context;

        public CarriersController(EnocaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Carriers.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Carrier carrier)
        {
            _context.Carriers.Add(carrier);
            await _context.SaveChangesAsync();
            return Ok("Kayıt başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Carrier carrier)
        {
            if (id != carrier.CarrierId) return BadRequest("ID eşleşmiyor.");

            _context.Carriers.Update(carrier);
            await _context.SaveChangesAsync();
            return Ok("Kayıt bilgileri güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var carrier = await _context.Carriers.FindAsync(id);
            if (carrier == null) return NotFound("Kargo firması bulunamadı.");

            _context.Carriers.Remove(carrier);
            await _context.SaveChangesAsync();
            return Ok($"{id} ID'li kayıt silindi.");
        }
    }
}