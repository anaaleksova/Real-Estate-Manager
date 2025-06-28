using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Domain.Identity;

namespace RealEstate.Domain.DomainModels
{
    public class Client:BaseEntity
    {
        public string ApplicationUserId { get; set; } 
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Appointment> Appointments { get; set; } 
        public ICollection<Favorite> Favorites { get; set; } 
    }
}
