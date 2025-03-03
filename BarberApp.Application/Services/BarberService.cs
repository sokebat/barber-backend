 
using BarberApp.Application.Interface;
using BarberApp.Domain;

namespace BarberApp.Application.Services
{
    public class BarberService : IBarberService
    {
        private readonly IBarberRepository _barberRepository;

        public BarberService(IBarberRepository barberRepository)
        {
            _barberRepository = barberRepository;
        }

        public async Task<List<Barber>> GetAllBarbers()
        {
            return await _barberRepository.GetAllBarbers();
        }

        public async Task<Barber> GetBarberById(int id)
        {
            return await _barberRepository.GetBarberById(id);
        }

        public async Task AddBarber(Barber barber)
        {
            await _barberRepository.AddBarber(barber);
        }
    }
}
