using BarberApp.Application.Interface;
using BarberApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberApp.Persistence.Repository
{
    public class AppointmentRepositiory : IAppointmentRepository
    {
        private readonly BarberDbContext _context;
        private readonly ILogger<AppointmentRepositiory> _logger;

        public AppointmentRepositiory(BarberDbContext context, ILogger<AppointmentRepositiory> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAppointments(Appointment appointment)
        {
            try
            {
                if(appointment == null)
                {
                    throw new ArgumentNullException(nameof(appointment), "Appointment cannot be null.");
                }
                await _context.Appointment.AddAsync(appointment);
                await _context.SaveChangesAsync();

            }catch(Exception ex)
            {
                _logger.LogError(ex, "Error adding a new appointment.");
                throw new Exception("An error occurred while adding the appointment.");

            }
        }

        public async Task DeleteAppointments(int id)
        {
            try
            {
                var appointments = await _context.Appointment.FindAsync(id);
                if(appointments == null)
                {
                    throw new KeyNotFoundException("Appointment not found.");
                }
                _context.Appointment.Remove(appointments);
                await _context.SaveChangesAsync();

            }catch(Exception ex)
            {
                _logger.LogError(ex, $"Error deleting appointment with ID {id}.");
                throw;

            }
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            try
            {
                return await _context.Appointment
                    .Include(a => a.Team)
                    .Include(a => a.Product)
                    .AsNoTracking()
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retriving all the appointmetns");
                throw new Exception("Dataabse error occured while retriving the appointments");

            }
        }

        public async Task<Appointment?> GetAllAppointmentsById(int id)
        {
            try
            {
                var appointments = await _context.Appointment
                    .Include(a => a.Team)
                    .Include(a => a.Product)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.id == id);

                if(appointments == null)
                {
                    throw new KeyNotFoundException($"Appointment with ID {id} not found.");
                    
                }
                return appointments;
                

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving appointment with ID {id}.");
                throw;

            }
        }

        public async Task UpdateAppointments(Appointment appointment)
        {
            try
            {
                if(appointment == null)
                {
                    throw new ArgumentNullException(nameof(appointment), "Appointment cannot be null.");
                }
                var existingAppointment = await _context.Appointment.FindAsync(appointment.id);
                if (existingAppointment == null)
                    throw new KeyNotFoundException("Appointment not found.");
                _context.Entry(existingAppointment).CurrentValues.SetValues(appointment);
                await _context.SaveChangesAsync();

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error updating appointment with ID {appointment.id}.");
                throw;

            }
        }
    }
}
