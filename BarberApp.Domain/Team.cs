namespace BarberApp.Domain
{
    public class Team
    {
        public int Id { get; set; }

        public required string Name { get; set; } 

        public required string Description { get; set; }

        public string? ProfileImageUrl { get; set; }  

        public required string Specialty { get; set; }  
    }
}
