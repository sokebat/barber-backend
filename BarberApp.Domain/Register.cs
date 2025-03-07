using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberApp.Domain
{
   public class Register
    {
        public string Email { get; set; }  // Ensure this exists
        public string FullName { get; set; }
        public string Password { get; set; }
    }
}
