using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberApp.Domain.Models
{
    public class OurServices
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Service image URL is required.")]
        public string ServiceImageUrl { get; set; }

        public List<OurServicesData> Data { get; set; } = new List<OurServicesData>();
    }

    public class OurServicesData
    {
        public int Id { get; set; }

        public string Image { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string Type { get; set; }
    }

}
