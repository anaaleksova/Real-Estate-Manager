using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Domain.Identity;

namespace RealEstate.Domain.DomainModels
{
    public class Property : BaseEntity
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public string AgentId { get; set; }
        public ApplicationUser Agent { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<PropertyInFavorite> Properties { get; set; }
    }


}
