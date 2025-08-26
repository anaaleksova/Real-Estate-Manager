using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Domain.DomainModels;

namespace RealEstate.Service.Interface
{
    public interface IExternalPropertyService
    {
        Task<List<Property>> FetchExternalProperties();
    }
}
