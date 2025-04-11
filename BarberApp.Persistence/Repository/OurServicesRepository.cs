using BarberApp.Application.Interface;
using BarberApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BarberApp.Persistence.Repository
{
    public class OurServicesRepository : IOurServicesRepository
    {
        private readonly BarberDbContext _context;
        private readonly ILogger<TeamRepository> _logger;

        public OurServicesRepository(BarberDbContext context, ILogger<TeamRepository> logger)
        {
            _context = context;
            _logger = logger;

        }

        async Task<List<OurServices>> IOurServicesRepository.GetAllServices()
        {
            try
            {
                return await _context.OurServices.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all team members.");
                throw new Exception("Database error occurred while retrieving team members.");

            }
        }
        async Task IOurServicesRepository.AddService(OurServices service)
        {
            try
            {
                if (service == null)
                    throw new ArgumentNullException(nameof(service), "service cannot be null.");
                await _context.OurServices.AddAsync(service);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding a new service member.");
                throw new Exception("An error occurred while adding the service.");
            }
        }

        async Task IOurServicesRepository.DeleteService(int id)
        {
            try
            {
                var service = _context.OurServices.Find(id);
                if (service == null)
                    throw new KeyNotFoundException($"Service with ID {id} not found.");

                _context.OurServices.Remove(service);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting service with ID {id}.");
                throw new Exception("An error occurred while deleting the service.");
            }



        }

        Task<OurServices> IOurServicesRepository.GetServiceById(int id)
        {
            try
            {
                var service = _context.OurServices.AsNoTracking().FirstOrDefault(t => t.Id == id);
                if (service == null)
                    throw new KeyNotFoundException($"Service with ID {id} not found.");
                return Task.FromResult(service);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving service with ID {id}.");
                throw;
            }
        }

        Task IOurServicesRepository.UpdateService(OurServices service)
        {
            try
            {
             if(service == null)
                    throw new ArgumentNullException(nameof(service), "service cannot be null.");

             var existingService = _context.OurServices.Find(service.Id);
                if (existingService == null)
                    throw new KeyNotFoundException($"Service with ID {service.Id} not found.");

                //_context.OurServices.Update(service);
                _context.Entry(existingService).CurrentValues.SetValues(service);
                return _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating service with ID {service.Id}.");
                throw new Exception("An error occurred while updating the service.");
            }
        }
    }
}