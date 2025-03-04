using BarberApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace BarberApp.Persistence
{
    public class BarberDbContext : DbContext
    {
        public BarberDbContext(DbContextOptions<BarberDbContext> options) : base(options)
        {
        }

        public DbSet<Team> Team { get; set; }
        public DbSet<OurServices> OurServices { get; set; }
    }
}
