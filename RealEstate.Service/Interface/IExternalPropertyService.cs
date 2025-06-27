using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service.Interface
{
    public interface IExternalPropertyService
    {
        Task ImportExternalProperties(string agentId);
    }
}
