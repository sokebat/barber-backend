using BarberApp.Domain.Models;

namespace BarberApp.Application.Interface
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAllAppointments();
        Task<Appointment?> GetAppointmentById(int id);
        Task AddAppointment(Appointment appointment);
        Task DeleteAppointment(int id);
        Task UpdateAppointment(Appointment appointment);
    }
}