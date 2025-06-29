using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Domain.DomainModels;
using RealEstate.Repository;
using RealEstate.Service.Interface;
using Newtonsoft.Json;
using RealEstate.Domain.DTO;

namespace RealEstate.Service.Implementation
{
    public class ExternalPropertyService : IExternalPropertyService
    {
        private readonly IRepository<Property> _propertyRepository;
        private readonly IRepository<AgentProperty> _agentPropertyRepository;
        private readonly HttpClient _httpClient;

        public ExternalPropertyService(
            IRepository<Property> propertyRepository,
            IRepository<AgentProperty> agentPropertyRepository)
        {
            _propertyRepository = propertyRepository;
            _agentPropertyRepository = agentPropertyRepository;
            _httpClient = new HttpClient();
        }

        public async Task ImportExternalProperties(Guid agentId)
        {
            var response = await _httpClient.GetAsync("https://api.example.com/properties");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch properties from external API.");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var externalProperties = JsonConvert.DeserializeObject<List<ExternalPropertyDTO>>(jsonData);

            foreach (var item in externalProperties)
            {
                var property = new Property
                {
                    Id = Guid.NewGuid(),
                    Title = item.Title,
                    Address = item.Location,
                    Description = item.Summary,
                    Price = item.Price,
                    Status = "Available"
                };

                var createdProperty = _propertyRepository.Insert(property);

                var agentProperty = new AgentProperty
                {
                    Id = Guid.NewGuid(),
                    AgentId = agentId,
                    PropertyId = createdProperty.Id
                };
                _agentPropertyRepository.Insert(agentProperty);
            }
        }
    }
}
