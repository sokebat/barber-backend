using BarberApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberApp.Application.Interface
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAllAppointments();
        Task<Appointment?> GetAllAppointmentsById(int id);
        Task AddAppointments(Appointment appointment);
        Task DeleteAppointments(int id);
        Task UpdateAppointments(Appointment appointment);
    }
}
