using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Domain.DomainModels;

namespace RealEstate.Service.Interface
{
    public interface IPropertyService
    {
        List<Property> GetAll();
        Property? GetById(Guid id);
        Property Add(Property property);
        Property Update(Property property);
        Property DeleteById(Guid id);
        List<Property> GetByAgent(string agentId);
    }
}
