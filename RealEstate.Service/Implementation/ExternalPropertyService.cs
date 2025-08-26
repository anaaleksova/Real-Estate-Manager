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
using System.Net.Http.Json;
using System.Net.Http;

namespace RealEstate.Service.Implementation
{
    public class ExternalPropertyService : IExternalPropertyService
    {
        private readonly IPropertyService _propertyService;
        private readonly IRepository<AgentProperty> _agentPropertyRepository;
        private readonly HttpClient _httpClient;

        public ExternalPropertyService(IHttpClientFactory httpClientFactory,
            IPropertyService propertyService,
            IRepository<AgentProperty> agentPropertyRepository)
        {
            _propertyService = propertyService;
            _agentPropertyRepository = agentPropertyRepository;
            _httpClient = new HttpClient();
        }

        public async Task<List<Property>> FetchExternalProperties()
        {
            var externalProperties = await _httpClient.GetFromJsonAsync<List<ExternalPropertyDTO>>("https://68947444be3700414e133d24.mockapi.io/properties");


            var newProperties = externalProperties.Select(x => new Property
            {
                Id = Guid.NewGuid(),
                Title = x.Title,
                Address = x.Location,
                Description = x.Summary,
                Price = x.Price,
                Status = "Available"
            }).ToList();

            _propertyService.InsertMany(newProperties);

            return newProperties;
        }
    }
}
