using EnocaChallenge.Core.Entities;
using EnocaChallenge.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnocaChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierConfigurationsController : ControllerBase
    {
        private readonly EnocaDbContext _context;

        public CarrierConfigurationsController(EnocaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.CarrierConfigurations.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CarrierConfiguration configuration)
        {
            _context.CarrierConfigurations.Add(configuration);
            await _context.SaveChangesAsync();
            return Ok("Kayıt başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CarrierConfiguration configuration)
        {
            if (id != configuration.CarrierConfigurationId) return BadRequest("ID eşleşmiyor.");

            _context.CarrierConfigurations.Update(configuration);
            await _context.SaveChangesAsync();
            return Ok("Kayıt bilgileri güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var configuration = await _context.CarrierConfigurations.FindAsync(id);
            if (configuration == null) return NotFound("Konfigürasyon bulunamadı.");

            _context.CarrierConfigurations.Remove(configuration);
            await _context.SaveChangesAsync();
            return Ok($"{id} ID'li kayıt silindi.");
        }
    }
}