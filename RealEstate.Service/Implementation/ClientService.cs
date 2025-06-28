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
    public class ClientService : IClientService
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<Appointment> _appointmentRepository;
        private readonly IFavoriteService _favoriteService;

        public ClientService(
            IRepository<Client> clientRepository,
            IRepository<Appointment> appointmentRepository,
            IFavoriteService favoriteService)
        {
            _clientRepository = clientRepository;
            _appointmentRepository = appointmentRepository;
            _favoriteService = favoriteService;
        }

        public Client GetClientByUserId(string userId)
        {
            return _clientRepository.Get(
                selector: c => c,
                predicate: c => c.ApplicationUserId == userId,
                include: x => x.Include(c => c.ApplicationUser));
        }

        public Client CreateClientProfile(string userId)
        {
            var existingClient = GetClientByUserId(userId);
            if (existingClient != null) return existingClient;

            var client = new Client
            {
                Id = Guid.NewGuid(),
                ApplicationUserId = userId
            };

            return _clientRepository.Insert(client);
        }

        public List<Property> GetFavoriteProperties(string userId)
        {
            var favorite = _favoriteService.GetFavoriteForUser(userId);
            return favorite?.PropertiesInFavourite?.Select(p => p.Property).ToList() ?? new List<Property>();
        }

        public List<Appointment> GetClientAppointments(string userId)
        {
            var client = GetClientByUserId(userId);
            if (client == null) return new List<Appointment>();

            return _appointmentRepository.GetAll(
                selector: a => a,
                predicate: a => a.ClientId == userId,
                include: x => x.Include(a => a.Property)
                              .Include(a => a.Agent)
                              .ThenInclude(agent => agent.ApplicationUser)).ToList();
        }
    }
}
