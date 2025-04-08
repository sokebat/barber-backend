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
        public DbSet<OurServicesData> OurServicesData { get; set; }
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure OurServices and OurServicesData relationship
            modelBuilder.Entity<OurServices>()
                .HasMany(s => s.Data)
                .WithOne()
                .HasForeignKey("OurServicesId")
                .OnDelete(DeleteBehavior.Cascade);

            // Configure a sequence for OurServicesData.Id to start at 123
            modelBuilder.Entity<OurServicesData>()
                .Property(d => d.Id)
                .ValueGeneratedOnAdd(); // Ensure auto-increment

            // Optional: If you want to start IDs at 123 globally
            modelBuilder.HasSequence<int>("OurServicesDataSequence")
                .StartsAt(123)
                .IncrementsBy(1);

            modelBuilder.Entity<OurServicesData>()
                .Property(d => d.Id)
                .HasDefaultValueSql("nextval('OurServicesDataSequence')");

            // Configure required properties for OurServices
            modelBuilder.Entity<OurServices>()
                .Property(s => s.Name)
                .IsRequired();
            modelBuilder.Entity<OurServices>()
                .Property(s => s.Description)
                .IsRequired();
            modelBuilder.Entity<OurServices>()
                .Property(s => s.ServiceImageUrl)
                .IsRequired();

            // Configure OurServicesData Price precision
            modelBuilder.Entity<OurServicesData>()
                .Property(d => d.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}