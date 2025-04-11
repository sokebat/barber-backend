using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarberApp.Persistence;
using BarberApp.Domain.Models;

namespace BarberApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OurServicesController : ControllerBase
    {
        private readonly BarberDbContext _context;

        public OurServicesController(BarberDbContext context)
        {
            _context = context;
        }

        // GET: api/OurServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OurServices>>> GetOurServices()
        {
            return await _context.OurServices.Include(s =>s.Data).ToListAsync();
        }

        // GET: api/OurServices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OurServices>> GetOurServices(int id)
        {
            var ourServices = await _context.OurServices.Include(s =>s.Data).FirstOrDefaultAsync(s=>s.Id ==id);

            if (ourServices == null)
            {
                return NotFound();
            }

            return ourServices;
        }

        // PUT: api/OurServices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOurServices(int id, OurServices ourServices)
        {
            if (id != ourServices.Id)
            {
                return BadRequest();
            }

            //_context.Entry(ourServices).State = EntityState.Modified;
            //_context.Entry(ourServices.Data).State = EntityState.Modified;
            var existingService = await _context.OurServices
                .Include(s => s.Data)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (existingService == null)
            {
                return NotFound();
            }

            // Update scalar properties
            _context.Entry(existingService).CurrentValues.SetValues(ourServices);

            // Handle the Data list
            // Remove deleted items
            foreach (var existingData in existingService.Data.ToList())
            {
                if (!ourServices.Data.Any(d => d.Id == existingData.Id))
                {
                    _context.OurServicesData.Remove(existingData);
                }
            }

            // Update or add items
            foreach (var data in ourServices.Data)
            {
                var existingData = existingService.Data.FirstOrDefault(d => d.Id == data.Id && d.Id != 0);
                if (existingData != null)
                {
                    _context.Entry(existingData).CurrentValues.SetValues(data);
                }
                else
                {
                    // Ensure new items don't have a pre-set Id
                    data.Id = 0; // Let EF Core generate the Id
                    existingService.Data.Add(data);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OurServicesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OurServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OurServices>> PostOurServices(OurServices ourServices)
        {
            if (ourServices == null)
            {
                return BadRequest("OurServices cannot be null.");
            }

            // Ensure Data items don't have pre-set IDs
            foreach (var data in ourServices.Data)
            {
                data.Id = 0; // Let EF Core generate the Id
            }

            _context.OurServices.Add(ourServices);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOurServices", new { id = ourServices.Id }, ourServices);
        }

        // DELETE: api/OurServices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOurServices(int id)
        {
            var ourServices = await _context.OurServices
                 .Include(s => s.Data)
                 .FirstOrDefaultAsync(s => s.Id == id);

            if (ourServices == null)
            {
                return NotFound();
            }

            _context.OurServices.Remove(ourServices);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OurServicesExists(int id)
        {
            return _context.OurServices.Any(e => e.Id == id);
        }
    }
}
