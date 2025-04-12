using System.ComponentModel.DataAnnotations;

namespace BarberApp.Domain.Models
{
    public class Team
    {
        public int Id { get; set; } // Changed from Guid to int for SQLite compatibility

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [StringLength(500, ErrorMessage = "Profile image URL cannot exceed 500 characters.")]
        public string? ProfileImageUrl { get; set; }

        [Required(ErrorMessage = "Specialty is required.")]
        [StringLength(100, ErrorMessage = "Specialty cannot exceed 100 characters.")]
        public string Specialty { get; set; }
    }
}
