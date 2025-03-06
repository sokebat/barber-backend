using BarberApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace BarberApp.Persistence
{
    public class BarberDbContext : DbContext
    {
       

        public BarberDbContext(DbContextOptions<BarberDbContext> options)
       : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=barber.db;");
            }
        }

        public DbSet<Team> Team { get; set; }
        public DbSet<OurServices> OurServices { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<User> User { get; set; }
    }
}
