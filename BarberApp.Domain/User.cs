
using Microsoft.AspNetCore.Identity;

namespace BarberApp.Domain
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string Address { get; set; }
        public string Role { get; set; } = "user";
    }
}
