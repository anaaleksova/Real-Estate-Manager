using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Domain.DomainModels;

namespace RealEstate.Service.Interface
{
    public interface IClientService
    {
        Client GetClientByUserId(string userId);
        Client CreateClientProfile(string userId);
        List<Property> GetFavoriteProperties(string userId);
        List<Appointment> GetClientAppointments(string userId);
    }
}
