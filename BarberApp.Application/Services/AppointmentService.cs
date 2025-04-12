using BarberApp.Application.Interface;
using BarberApp.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberApp.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(IAppointmentRepository appointmentRepository, ILogger<AppointmentService> logger)
        {
            _appointmentRepository = appointmentRepository;
            _logger = logger;
        }

        public async Task AddAppointment(Appointment appointment)
        {
            try
            {
                await _appointmentRepository.AddAppointment(appointment);
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
                await _appointmentRepository.DeleteAppointment(id);
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
                return await _appointmentRepository.GetAllAppointments();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all appointments.");
                throw;
            }
        }

        public async Task<Appointment> GetAppointmentById(int id)
        {
            try
            {
                var appointment = await _appointmentRepository.GetAppointmentById(id);
                if (appointment == null)
                {
                    throw new KeyNotFoundException($"Appointment with ID {id} not found.");
                }
                return appointment;
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
                await _appointmentRepository.UpdateAppointment(appointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating appointment with ID {appointment.Id}.");
                throw;
            }
        }
    }
}