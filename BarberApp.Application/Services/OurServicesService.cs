using BarberApp.Application.Interface;
using BarberApp.Domain.Models;
using Microsoft.Extensions.Logging;


namespace BarberApp.Application.Services
{
    public class OurServicesService : IOurServicesService
    {
        private readonly IOurServicesRepository _ourServicesRepository;
        private readonly ILogger<OurServicesService> _logger;

        public OurServicesService(IOurServicesRepository ourServicesRepository, ILogger<OurServicesService> logger)
        {
            _ourServicesRepository = ourServicesRepository;
            _logger = logger;
        }

        public async Task AddService(OurServices service)
        {
            try
            {
                await _ourServicesRepository.AddService(service);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding a new service.");
                throw;
            }
        }

        public async Task DeleteService(int id)
        {
            try
            {
                await _ourServicesRepository.DeleteService(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting a service.");
                throw;
            }
        }

        public async Task<List<OurServices>> GetAllServices()
        {
            try
            {
                return await _ourServicesRepository.GetAllServices();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all services.");
                throw;
            }
        }

        public async Task<OurServices> GetServiceById(int id)
        {
            try
            {
                return await _ourServicesRepository.GetServiceById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving service with ID {id}.");
                throw;
            }
        }

        public async Task UpdateService(OurServices service)
        {
            try
            {
                await _ourServicesRepository.UpdateService(service);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating a service.");
                throw;
            }
        }
    }
}
