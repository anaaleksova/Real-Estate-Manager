using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.DTO
{
    public class ExternalPropertyDTO
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public string Summary { get; set; }
        public decimal Price { get; set; }
    }
}
