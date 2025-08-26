using Microsoft.AspNetCore.Mvc;
using RealEstate.Domain.DomainModels;
using RealEstate.Service.Interface;

namespace RealEstate.Web.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IAgentService _agentService;
        private readonly IExternalPropertyService _externalPropertyService;

        public PropertiesController(IPropertyService propertyService, IAgentService agentService, IExternalPropertyService externalPropertyService)
        {
            _propertyService = propertyService;
            _agentService = agentService;
            _externalPropertyService = externalPropertyService;
        }

        // GET: Properties
        public IActionResult Index()
        {
            return View(_propertyService.GetAll());
        }

        // GET: Properties/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var property = _propertyService.GetById(id.Value);
            if (property == null)
            {
                return NotFound();
            }
            var agents = _agentService.GetAgentsForProperty(id.Value);
            ViewBag.Agents = agents;

            return View(property);
        }

        // GET: Properties/Create
        public IActionResult Create()
        {
            ViewBag.AllAgents = _agentService.GetAll();
            return View();
        }

        // POST: Properties/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Property property, List<Guid> SelectedAgentIds)
        {
            if (ModelState.IsValid)
            {
                var createdProperty = _propertyService.Add(property);
                if (SelectedAgentIds != null && SelectedAgentIds.Any())
                {
                    foreach (var agentId in SelectedAgentIds)
                    {
                        _agentService.AssignAgentToProperty(agentId, createdProperty.Id);
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.AllAgents = _agentService.GetAll();
            return View(property);
        }

        // GET: Properties/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var property = _propertyService.GetById(id.Value);
            if (property == null)
            {
                return NotFound();
            }

            ViewBag.AllAgents = _agentService.GetAll();
            ViewBag.CurrentAgentIds = property.AgentProperties?.Select(ap => ap.AgentId).ToList() ?? new List<Guid>();

            return View(property);
        }

        // POST: Properties/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Property property, List<Guid> SelectedAgentIds)
        {
            if (id != property.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _propertyService.Update(property);

                var currentAgents = _agentService.GetAgentsForProperty(property.Id);
                var currentAgentIds = currentAgents.Select(a => a.Id).ToList();

                foreach (var agentId in currentAgentIds)
                {
                    if (SelectedAgentIds == null || !SelectedAgentIds.Contains(agentId))
                    {
                        _agentService.RemoveAgentFromProperty(agentId, property.Id);
                    }
                }

                if (SelectedAgentIds != null)
                {
                    foreach (var agentId in SelectedAgentIds)
                    {
                        if (!currentAgentIds.Contains(agentId))
                        {
                            _agentService.AssignAgentToProperty(agentId, property.Id);
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.AllAgents = _agentService.GetAll();
            ViewBag.CurrentAgentIds = SelectedAgentIds ?? new List<Guid>();
            return View(property);
        }

        // GET: Properties/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var property = _propertyService.GetById(id.Value);
            if (property == null)
            {
                return NotFound();
            }

            return View(property);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _propertyService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> FetchProperties()
        {
            await _externalPropertyService.FetchExternalProperties();
            return RedirectToAction(nameof(Index));
        }
    }
}