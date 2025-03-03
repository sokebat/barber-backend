using BarberApp.Application.Interface;
using BarberApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace BarberApp.Persistence.Repository
{
    public class BarberRepository : IBarberRepository
    {
        private readonly BarberDbContext _context;

        public BarberRepository(BarberDbContext context)
        {
            _context = context;
        }

        public async Task<List<Barber>> GetAllBarbers()
        {
            return await _context.Barbers.ToListAsync();
        }

        public async Task<Barber> GetBarberById(int id)
        {
            return await _context.Barbers.FindAsync(id);
        }

        public async Task AddBarber(Barber barber)
        {
            _context.Barbers.Add(barber);
            await _context.SaveChangesAsync();
        }
    }

}