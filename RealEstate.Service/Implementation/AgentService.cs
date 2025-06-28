using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.DomainModels;
using RealEstate.Repository;
using RealEstate.Service.Interface;

namespace RealEstate.Service.Implementation
{
    public class AgentService : IAgentService
    {
        private readonly IRepository<Agent> _agentRepository;
        private readonly IRepository<AgentProperty> _agentPropertyRepository;
        private readonly IRepository<Appointment> _appointmentRepository;

        public AgentService(
            IRepository<Agent> agentRepository,
            IRepository<AgentProperty> agentPropertyRepository,
            IRepository<Appointment> appointmentRepository)
        {
            _agentRepository = agentRepository;
            _agentPropertyRepository = agentPropertyRepository;
            _appointmentRepository = appointmentRepository;
        }

        public Agent GetAgentByUserId(string userId)
        {
            return _agentRepository.Get(
                selector: a => a,
                predicate: a => a.ApplicationUserId == userId,
                include: x => x.Include(a => a.ApplicationUser));
        }

        public Agent CreateAgentProfile(string userId)
        {
            var existingAgent = GetAgentByUserId(userId);
            if (existingAgent != null) return existingAgent;

            var agent = new Agent
            {
                Id = Guid.NewGuid(),
                ApplicationUserId = userId
            };

            return _agentRepository.Insert(agent);
        }

        public List<Agent> GetAgentsForProperty(Guid propertyId)
        {
            return _agentPropertyRepository.GetAll(
                selector: ap => ap.Agent,
                predicate: ap => ap.PropertyId == propertyId,
                include: x => x.Include(ap => ap.Agent)
                              .ThenInclude(a => a.ApplicationUser)).ToList();
        }

        public List<Appointment> GetAgentAppointments(string userId)
        {
            var agent = GetAgentByUserId(userId);
            if (agent == null) return new List<Appointment>();

            return _appointmentRepository.GetAll(
                selector: a => a,
                predicate: a => a.AgentId == agent.Id,
                include: x => x.Include(a => a.Property)
                              .Include(a => a.Client)
                              .ThenInclude(c => c.ApplicationUser)).ToList();
        }

        public void AssignAgentToProperty(Guid agentId, Guid propertyId)
        {
            var exists = _agentPropertyRepository.Get(
                selector: ap => ap,
                predicate: ap => ap.AgentId == agentId && ap.PropertyId == propertyId);

            if (exists == null)
            {
                var agentProperty = new AgentProperty
                {
                    Id = Guid.NewGuid(),
                    AgentId = agentId,
                    PropertyId = propertyId
                };
                _agentPropertyRepository.Insert(agentProperty);
            }
        }

        public void RemoveAgentFromProperty(Guid agentId, Guid propertyId)
        {
            var agentProperty = _agentPropertyRepository.Get(
                selector: ap => ap,
                predicate: ap => ap.AgentId == agentId && ap.PropertyId == propertyId);

            if (agentProperty != null)
            {
                _agentPropertyRepository.Delete(agentProperty);
            }
        }
    }
}
