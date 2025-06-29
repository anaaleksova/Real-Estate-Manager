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
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<Property> Properties { get; set; } = new List<Property>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<AgentProperty> AgentProperties { get; set; } = new List<AgentProperty>();
    }
}
