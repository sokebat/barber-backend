using BarberApp.Domain.Models;

namespace BarberApp.Application.Interface
{
    public interface IOurServicesRepository
    {
        Task<List<OurServices>> GetAllServices();
        Task<OurServices> GetServiceById(int id);
        Task AddService(OurServices service);
        Task DeleteService(int id);
        Task UpdateService(OurServices service);
    }
}
