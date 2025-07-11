﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            appointment.Status = "Scheduled";
            var result = _appointmentRepository.Insert(appointment);

            var fullAppointment = GetById(result.Id);
            if (fullAppointment != null)
            {
                SendEmailReminder(fullAppointment);
            }

            return result;
        }

        public Appointment DeleteById(Guid id)
        {
            var appointment = _appointmentRepository.Get(
                selector: x => x,
                predicate: x => x.Id == id);
            return _appointmentRepository.Delete(appointment);
        }

        public List<Appointment> GetAll()
        {
            return _appointmentRepository.GetAll(
                selector: x => x,
                include: x => x.Include(a => a.Property)
                              .Include(a => a.Client)
                              .Include(a => a.Agent)).ToList();
        }

        public List<Appointment> GetUserAppointments(string userId)
        {
            return _appointmentRepository.GetAll(
                selector: a => a,
                predicate: a => a.ClientId== userId,
                include: x => x.Include(a => a.Property)
                              .Include(a => a.Agent)
                              .Include(a => a.Client)).ToList();
        }

        public Appointment GetById(Guid id)
        {
            return _appointmentRepository.Get(
                selector: x => x,
                predicate: x => x.Id == id,
                include: x => x.Include(a => a.Property)
                              .Include(a => a.Client)
                              .Include(a => a.Agent));
        }

        public Appointment Update(Appointment appointment)
        {
            return _appointmentRepository.Update(appointment);
        }

        public void SendEmailReminder(Appointment appointment)
        {
            if (appointment.Client == null || appointment.Property == null)
                return;

            var clientEmail = new EmailMessage
            {
                Subject = "Property Inspection Scheduled",
                MailTo = appointment.Client.Email,
                Content = $"Dear {appointment.Client.FullName},\n\n" +
                          $"Your property inspection is scheduled on {appointment.ScheduledDate:dd MMM yyyy HH:mm} " +
                          $"for property '{appointment.Property.Title}' at {appointment.Property.Address}.\n" +
                          $"Agent: {appointment.Agent.Name}\n" +
                          $"Best regards,\nReal Estate Team"
            };

            _emailService.SendEmailAsync(clientEmail);
        }

        public Appointment CreateAppointmentWithAgent(Guid propertyId, string userId, Guid agentId, DateTime scheduledDate, string notes = "")
        {
            var appointment = new Appointment
            {
                PropertyId = propertyId,
                ClientId= userId,
                AgentId = agentId,
                ScheduledDate = scheduledDate,
            };

            return Add(appointment);
        }
    }
}