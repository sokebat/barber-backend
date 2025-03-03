using BarberApp.Application.Interface;
using BarberApp.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BarberApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarberController : ControllerBase
    {
        private readonly IBarberService _barberService;

        public BarberController(IBarberService barberService)
        {
            _barberService = barberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var barbers = await _barberService.GetAllBarbers();
            return Ok(barbers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var barber = await _barberService.GetBarberById(id);
            if (barber == null)
            {
                return NotFound();
            }
            return Ok(barber);
        }

        [HttpPost]
        public async Task<IActionResult> AddBarber([FromBody] Barber barber)
        {
            await _barberService.AddBarber(barber);
            return CreatedAtAction(nameof(GetById), new { id = barber.Id }, barber);
        }
    }
}