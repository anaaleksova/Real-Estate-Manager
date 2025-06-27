using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RealEstate.Domain.DomainModels;

namespace RealEstate.Domain.Identity
{

    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public bool IsAgent { get; set; }
        public string Email { get; set; }

        public ICollection<Property> Properties { get; set; }  // Само ако е агент
        public ICollection<Appointment> Appointments { get; set; } // За агент или клиент
        public ICollection<Favorite> Favorites { get; set; }  // Само ако е клиент
    }
}
