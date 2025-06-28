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
        Agent GetAgentByUserId(string userId);
        Agent CreateAgentProfile(string userId);
        List<Agent> GetAgentsForProperty(Guid propertyId);
        List<Appointment> GetAgentAppointments(string userId);
        void AssignAgentToProperty(Guid agentId, Guid propertyId);
        void RemoveAgentFromProperty(Guid agentId, Guid propertyId);
    }
}
