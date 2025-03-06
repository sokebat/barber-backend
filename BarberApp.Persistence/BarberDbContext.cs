using BarberApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace BarberApp.Persistence
{
    public class BarberDbContext : IdentityDbContext<ApplicationUser> 
    {
       

        public BarberDbContext(DbContextOptions<BarberDbContext> options) : base(options)
        {

        }

        public BarberDbContext()
        {

        }


        public DbSet<Team> Team { get; set; }
        public DbSet<OurServices> OurServices { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Appointment> Appointment { get; set; }

       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=barber.db");
            }
        }
    }
}
