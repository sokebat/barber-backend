namespace BarberApp.Domain.Models
{
    public class Team
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 

        public required string Name { get; set; }

        public required string Description { get; set; }

        public string? ProfileImageUrl { get; set; }

        public required string Specialty { get; set; }
    }
}
