using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public string ClientId { get; set; }

        public ApplicationUser Client { get; set; }

        public Guid AgentId { get; set; }
        public Agent Agent { get; set; }

        public DateTime ScheduledDate { get; set; }
        public string Status { get; set; } = "Scheduled";
    }



}
