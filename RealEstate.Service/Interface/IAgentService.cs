using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Domain.DomainModels;

namespace RealEstate.Service.Interface
{
    public interface IAgentService
    {
        List<Agent> GetAll();
        Agent GetById(Guid id);
        Agent Add(Agent agent);
        Agent Update(Agent agent);
        Agent DeleteById(Guid id);
        List<Agent> GetAgentsForProperty(Guid propertyId);
        List<Appointment> GetAgentAppointments(string userId);
        void AssignAgentToProperty(Guid agentId, Guid propertyId);
        void RemoveAgentFromProperty(Guid agentId, Guid propertyId);
        List<Property> GetPropertiesForAgent(Guid agentId);
    }
}
