using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Domain.Identity;

namespace RealEstate.Domain.DomainModels
{
    public class Agent:BaseEntity
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Property> Properties { get; set; }  
        public ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<AgentProperty> AgentProperties { get; set; }
    }
}
