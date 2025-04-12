using BarberApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberApp.Application.Interface
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAllAppointments();
        Task<Appointment> GetAppointmentById(int id);
        Task AddAppointment(Appointment appointment);
        Task DeleteAppointment(int id);
        Task UpdateAppointment(Appointment appointment);
    }
}