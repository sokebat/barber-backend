using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberApp.Domain
{
    public class OurServices
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ServiceImageUrl { get; set; }

        public List<OurServicesData> Data { get; set; } = new List<OurServicesData>();

    }

    public class OurServicesData
    {
        public int Id { get; set; }
       
        public string Image { get; set; }
        public string Title { get; set; }
        public string  Subtitle { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }

    }

}
