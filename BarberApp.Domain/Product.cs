
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberApp.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public required decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal discoungPrice { get; set; }
        public required string ImageUrl { get; set; }
        
        public required string CategoryName { get; set; }

    }
}
