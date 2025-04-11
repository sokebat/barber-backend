using BarberApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
