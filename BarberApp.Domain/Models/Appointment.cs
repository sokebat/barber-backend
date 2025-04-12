using System.ComponentModel;

namespace BarberApp.Domain.Models
{
    public class Appointment
    {
        public int Id { get; set; } // Database uses int
        public string ServiceName { get; set; } = string.Empty;
        public string SpecialistName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string AppointmentDate { get; set; } = string.Empty; // Store as YYYY-MM-DD
        public string AppointmentTime { get; set; } = string.Empty; // Store as HH:MM

        [DefaultValue(false)]
        public bool IsApproved { get; set; }
    }
}
