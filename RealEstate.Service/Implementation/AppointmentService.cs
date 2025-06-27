using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Domain;
using RealEstate.Domain.DomainModels;
using RealEstate.Repository;
using RealEstate.Service.Interface;

namespace RealEstate.Service.Implementation
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepository<Appointment> _appointmentRepository;
        private readonly IEmailService _emailService;

        public AppointmentService(IRepository<Appointment> appointmentRepository, IEmailService emailService)
        {
            _appointmentRepository = appointmentRepository;
            _emailService = emailService;
        }

        public Appointment Add(Appointment appointment)
        {
            appointment.Id = Guid.NewGuid();
            _appointmentRepository.Insert(appointment);
            SendEmailReminder(appointment);
            return appointment;
        }

        public Appointment DeleteById(Guid id)
        {
            var appointment = _appointmentRepository.Get(selector: x => x,
                                               predicate: x => x.Id == id);
            return _appointmentRepository.Delete(appointment);
        }

        public List<Appointment> GetAll()
        {
            return _appointmentRepository.GetAll(selector: x => x).ToList();
        }

        public Appointment GetById(Guid id)
        {
            return _appointmentRepository.Get(selector: x => x,
                                           predicate: x => x.Id == id);
        }

        public Appointment Update(Appointment appointment)
        { 
                return _appointmentRepository.Update(appointment); 
        }

        public void SendEmailReminder(Appointment appointment)
        {
            var email = new EmailMessage
            {
                Subject = "Inspection Scheduled",
                MailTo = appointment.Client.Email,
                Content = $"Dear {appointment.Client.FirstName} {appointment.Client.LastName},\n\n" +
                          $"Your property inspection is scheduled on {appointment.ScheduledDate:dd MMM yyyy HH:mm} " +
                          $"for property '{appointment.Property.Title}' with agent {appointment.Agent.FirstName} {appointment.Agent.LastName}."
            };

            _emailService.SendEmailAsync(email);
        }
    }

}
