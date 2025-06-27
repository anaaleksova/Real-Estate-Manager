using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Domain.Identity;

namespace RealEstate.Domain.DomainModels
{
    public class Appointment : BaseEntity
    {
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }
        public string AgentId { get; set; }
        public ApplicationUser Agent { get; set; }

        public string ClientId { get; set; }
        public ApplicationUser Client { get; set; }

        public DateTime ScheduledDate { get; set; }
        public string Status { get; set; }
    }



}
