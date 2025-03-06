using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BarberApp.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
