 
using BarberApp.Domain;

namespace BarberApp.Application.Interface
{
    public interface IBarberService
    {
        Task<List<Barber>> GetAllBarbers();
        Task<Barber> GetBarberById(int id);
        Task AddBarber(Barber barber);
    }
}
