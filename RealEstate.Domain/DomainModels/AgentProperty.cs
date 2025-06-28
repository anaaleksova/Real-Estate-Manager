using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.DomainModels
{
    public class AgentProperty:BaseEntity
    {
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }

        public Guid AgentId { get; set; }
        public Agent Agent { get; set; }
    }
}
