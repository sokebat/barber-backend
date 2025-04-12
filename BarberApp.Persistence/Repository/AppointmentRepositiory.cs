using BarberApp.Application.Interface;
using BarberApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberApp.Persistence.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly BarberDbContext _context;
        private readonly ILogger<AppointmentRepository> _logger;

        public AppointmentRepository(BarberDbContext context, ILogger<AppointmentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAppointment(Appointment appointment)
        {
            try
            {
                if (appointment == null)
                {
                    throw new ArgumentNullException(nameof(appointment), "Appointment cannot be null.");
                }

                // Validate date and time formats
                if (!DateTime.TryParse(appointment.AppointmentDate, out _))
                {
                    throw new ArgumentException("Invalid date format. Use YYYY-MM-DD.", nameof(appointment.AppointmentDate));
                }
                if (!TimeSpan.TryParse(appointment.AppointmentTime, out _))
                {
                    throw new ArgumentException("Invalid time format. Use HH:MM.", nameof(appointment.AppointmentTime));
                }

                await _context.Appointment.AddAsync(appointment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding a new appointment.");
                throw;
            }
        }

        public async Task DeleteAppointment(int id)
        {
            try
            {
                var appointment = await _context.Appointment.FindAsync(id);
                if (appointment == null)
                {
                    throw new KeyNotFoundException($"Appointment with ID {id} not found.");
                }
                _context.Appointment.Remove(appointment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting appointment with ID {id}.");
                throw;
            }
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            try
            {
                return await _context.Appointment.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all appointments.");
                throw;
            }
        }

        public async Task<Appointment?> GetAppointmentById(int id)
        {
            try
            {
                return await _context.Appointment.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving appointment with ID {id}.");
                throw;
            }
        }

        public async Task UpdateAppointment(Appointment appointment)
        {
            try
            {
                if (appointment == null)
                {
                    throw new ArgumentNullException(nameof(appointment), "Appointment cannot be null.");
                }

                var existingAppointment = await _context.Appointment.FindAsync(appointment.Id);
                if (existingAppointment == null)
                {
                    throw new KeyNotFoundException($"Appointment with ID {appointment.Id} not found.");
                }

                // Validate date and time formats
                if (!DateTime.TryParse(appointment.AppointmentDate, out _))
                {
                    throw new ArgumentException("Invalid date format. Use YYYY-MM-DD.", nameof(appointment.AppointmentDate));
                }
                if (!TimeSpan.TryParse(appointment.AppointmentTime, out _))
                {
                    throw new ArgumentException("Invalid time format. Use HH:MM.", nameof(appointment.AppointmentTime));
                }

                _context.Entry(existingAppointment).CurrentValues.SetValues(appointment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating appointment with ID {appointment.Id}.");
                throw;
            }
        }
    }
}