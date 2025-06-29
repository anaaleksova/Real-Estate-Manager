using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Domain.DomainModels;

namespace RealEstate.Service.Interface
{
    public interface IAppointmentService
    {
        List<Appointment> GetAll();
        List<Appointment> GetUserAppointments(string userId);
        Appointment GetById(Guid id);
        Appointment Add(Appointment appointment);
        Appointment Update(Appointment appointment);
        Appointment DeleteById(Guid id);
        void SendEmailReminder(Appointment appointment);
    }

}
