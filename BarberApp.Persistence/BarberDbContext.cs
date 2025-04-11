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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // No fallback needed since Program.cs configures SQL Server
            // Optionally, add for migration tools (not recommended for production):
            // if (!optionsBuilder.IsConfigured)
            // {
            //     optionsBuilder.UseSqlServer("Server=localhost;Database=BarberDb;Trusted_Connection=True;TrustServerCertificate=True;");
            // }
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

            //
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd(); // Ensures Id is auto-incrementing
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            });

            // Configure sequence for OurServicesData.Id
            modelBuilder.HasSequence<int>("OurServicesDataSequence")
                .StartsAt(123)
                .IncrementsBy(1);

            modelBuilder.Entity<OurServicesData>()
                .Property(d => d.Id)
                .HasDefaultValueSql("NEXT VALUE FOR OurServicesDataSequence");

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

            // Configure Product Price and DiscountPrice precision
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.DiscountPrice)
                .HasColumnType("decimal(18,2)");
        }
    }
}