using System.ComponentModel;

namespace BarberApp.Domain.Models
{
   public class Appointment
    {
        public int id { get; set; }

        public  required string ServiceName { get; set; }
        public required string SpecialistName { get; set; }


        public required string CustomerName { get; set; }
       public required DateTime AppointmentDate { get; set; }
       public required TimeSpan AppointmentTime { get; set; }

        [DefaultValue(false)]
        public bool IsApproved { get; set; } = false;

    }
}
