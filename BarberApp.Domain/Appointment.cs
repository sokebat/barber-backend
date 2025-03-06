namespace BarberApp.Domain
{
   public class Appointment
    {
        public int id { get; set; }

      
       public required int TeamId { get; set; }
       public  required string ServiceName { get; set; }

       public required string CustomerName { get; set; }
       public required DateTime AppointmentDate { get; set; }
       public required TimeSpan AppointmentTime { get; set; }

    }
}
