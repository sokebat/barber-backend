// BarberApp.Persistence/BarberDbContext.cs
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
                entity.Property(t => t.Name).IsRequired().HasMaxLength(100);
                entity.Property(t => t.Description).IsRequired().HasMaxLength(500);
                entity.Property(t => t.Specialty).IsRequired().HasMaxLength(100);
                entity.Property(t => t.ProfileImageUrl).HasMaxLength(500);
            });

            // OurServices and OurServicesData relationship
            modelBuilder.Entity<OurServices>()
                .HasMany(s => s.Data)
                .WithOne()
                .HasForeignKey("OurServicesId")
                .OnDelete(DeleteBehavior.Cascade);

            // Category configuration
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            });

            // OurServicesData sequence
            modelBuilder.HasSequence<int>("OurServicesDataSequence")
                .StartsAt(123)
                .IncrementsBy(1);

            modelBuilder.Entity<OurServicesData>()
                .Property(d => d.Id)
                .HasDefaultValueSql("NEXT VALUE FOR OurServicesDataSequence");

            // OurServices required properties
            modelBuilder.Entity<OurServices>()
                .Property(s => s.Name)
                .IsRequired();
            modelBuilder.Entity<OurServices>()
                .Property(s => s.Description)
                .IsRequired();
            modelBuilder.Entity<OurServices>()
                .Property(s => s.ServiceImageUrl)
                .IsRequired();

            // OurServicesData Price precision
            modelBuilder.Entity<OurServicesData>()
                .Property(d => d.Price)
                .HasColumnType("decimal(18,2)");

            // Product Price and DiscountPrice precision
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>()
                .Property(p => p.DiscountPrice)
                .HasColumnType("decimal(18,2)");

            
        }
    }
}