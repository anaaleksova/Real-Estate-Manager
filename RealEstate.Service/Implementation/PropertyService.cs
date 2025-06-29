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
    public class PropertyService : IPropertyService
    {
        private readonly IRepository<Property> _propertyRepository;

        public PropertyService(IRepository<Property> propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public Property Add(Property property)
        {
            property.Id = Guid.NewGuid();
            return _propertyRepository.Insert(property);
        }

        public Property DeleteById(Guid id)
        {
            var property = _propertyRepository.Get(
                selector: x => x,
                predicate: x => x.Id == id
            );

            return _propertyRepository.Delete(property);
        }

        public List<Property> GetAll()
        {
            return _propertyRepository.GetAll(
                selector: x => x,
                include: x => x.Include(p => p.AgentProperties)
                              .ThenInclude(ap => ap.Agent)).ToList();
        }

        public Property? GetById(Guid id)
        {
            return _propertyRepository.Get(
                selector: x => x,
                predicate: x => x.Id == id,
                include: x => x.Include(p => p.AgentProperties)
                              .ThenInclude(ap => ap.Agent));
        }

        public Property Update(Property property)
        {
            return _propertyRepository.Update(property);
        }
    }
}
