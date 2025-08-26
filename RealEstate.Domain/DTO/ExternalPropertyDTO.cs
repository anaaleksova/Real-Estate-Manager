using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.DTO
{
    public class ExternalPropertyDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Summary { get; set; }
        public int Price { get; set; }
        public int Rooms { get; set; }
        public int Area { get; set; }
        public string Type { get; set; }
    }
}
