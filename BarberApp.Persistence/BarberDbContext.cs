using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BarberApp.Domain.Models;

namespace BarberApp.Persistence
{
    public class BarberDbContext : IdentityDbContext<ApplicationUser>
    {
        public BarberDbContext(DbContextOptions<BarberDbContext> options) : base(options)
        {
        }

        public DbSet<Team> Team { get; set; }
        public DbSet<OurServices> OurServices { get; set; }
        public DbSet<OurServicesData> OurServicesData { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Appointment> Appointment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Team configuration
            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd(); // SQLite AUTOINCREMENT
                entity.Property(t => t.Name).IsRequired().HasMaxLength(100);
                entity.Property(t => t.Description).IsRequired().HasMaxLength(500);
                entity.Property(t => t.Specialty).IsRequired().HasMaxLength(100);
                entity.Property(t => t.ProfileImageUrl).HasMaxLength(500);
            });

            // OurServices configuration
            modelBuilder.Entity<OurServices>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Id).ValueGeneratedOnAdd();
                entity.Property(s => s.Name).IsRequired();
                entity.Property(s => s.Description).IsRequired();
                entity.Property(s => s.ServiceImageUrl).IsRequired();
            });

            // OurServicesData configuration
            modelBuilder.Entity<OurServicesData>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Id).ValueGeneratedOnAdd();
                entity.Property(d => d.Price).HasColumnType("decimal(18,2)");
                entity.Property(d => d.Image).HasMaxLength(500);
                entity.Property(d => d.Title).HasMaxLength(100);
                entity.Property(d => d.Subtitle).HasMaxLength(200);
                entity.Property(d => d.Type).HasMaxLength(50);
                // Foreign key to OurServices
                entity.HasOne<OurServices>()
                    .WithMany(s => s.Data)
                    .HasForeignKey("OurServicesId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Category configuration
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            });

            // Product configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Description).IsRequired().HasMaxLength(500);
                entity.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(p => p.DiscountPrice).HasColumnType("decimal(18,2)");
                entity.Property(p => p.ImageUrl).IsRequired().HasMaxLength(200);
                entity.Property(p => p.CategoryName).IsRequired().HasMaxLength(50);
            });

            // Appointment configuration
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Id).ValueGeneratedOnAdd();
                entity.Property(a => a.ServiceName).HasMaxLength(100);
                entity.Property(a => a.SpecialistName).HasMaxLength(100);
                entity.Property(a => a.CustomerName).HasMaxLength(100);
                entity.Property(a => a.AppointmentDate).HasMaxLength(10); // YYYY-MM-DD
                entity.Property(a => a.AppointmentTime).HasMaxLength(5); // HH:MM
            });
        }
    }
}