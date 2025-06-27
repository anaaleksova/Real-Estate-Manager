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
        private readonly HttpClient _httpClient;

        public ExternalPropertyService(IRepository<Property> propertyRepository)
        {
            _propertyRepository = propertyRepository;
            _httpClient = new HttpClient(); // Can be injected via DI
        }

        public async Task ImportExternalProperties(string agentId)
        {
            var response = await _httpClient.GetAsync("https://api.example.com/properties"); // Replace with real API
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
                    Status = "Available",
                    AgentId = agentId
                };

                _propertyRepository.Insert(property);
            }
        }
    }
}
