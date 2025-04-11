using Microsoft.AspNetCore.Identity;

namespace BarberApp.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Role { get; set; } // e.g., "Customer", "Barber", "Admin"
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsActive { get; set; }
    }
}
