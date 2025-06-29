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
        private readonly IRepository<Property> _propertyRepository;

        public AgentService(
            IRepository<Agent> agentRepository,
            IRepository<AgentProperty> agentPropertyRepository,
            IRepository<Property> propertyRepository)
        {
            _agentRepository = agentRepository;
            _agentPropertyRepository = agentPropertyRepository;
            _propertyRepository = propertyRepository;
        }

        public List<Agent> GetAll()
        {
            return _agentRepository.GetAll(selector: a => a).ToList();
        }

        public Agent GetById(Guid id)
        {
            return _agentRepository.Get(
                selector: a => a,
                predicate: a => a.Id == id);
        }

        public Agent Add(Agent agent)
        {
            agent.Id = Guid.NewGuid();
            return _agentRepository.Insert(agent);
        }

        public Agent Update(Agent agent)
        {
            return _agentRepository.Update(agent);
        }

        public Agent DeleteById(Guid id)
        {
            var agent = _agentRepository.Get(
                selector: a => a,
                predicate: a => a.Id == id);

            if (agent != null)
            {
                return _agentRepository.Delete(agent);
            }
            return null;
        }

        public List<Agent> GetAgentsForProperty(Guid propertyId)
        {
            return _agentPropertyRepository.GetAll(
                selector: ap => ap.Agent,
                predicate: ap => ap.PropertyId == propertyId,
                include: x => x.Include(ap => ap.Agent)).ToList();
        }

        public List<Property> GetPropertiesForAgent(Guid agentId)
        {
            return _agentPropertyRepository.GetAll(
                selector: ap => ap.Property,
                predicate: ap => ap.AgentId == agentId,
                include: x => x.Include(ap => ap.Property)).ToList();
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

        public List<Appointment> GetAgentAppointments(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
