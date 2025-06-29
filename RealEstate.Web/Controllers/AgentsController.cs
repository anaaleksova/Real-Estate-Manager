using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Domain.DomainModels;
using RealEstate.Service.Interface;
using System.Security.Claims;

namespace RealEstate.Web.Controllers
{
    public class AgentsController : Controller
    {
        private readonly IAgentService _agentService;
        private readonly IPropertyService _propertyService;

        public AgentsController(IAgentService agentService, IPropertyService propertyService)
        {
            _agentService = agentService;
            _propertyService = propertyService;
        }

        // GET: Agent
        public IActionResult Index()
        {
            var agents = _agentService.GetAll();
            return View(agents);
        }

        // GET: Agent/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = _agentService.GetById(id.Value);
            if (agent == null)
            {
                return NotFound();
            }

            ViewBag.Properties = _agentService.GetPropertiesForAgent(id.Value);
            return View(agent);
        }

        // GET: Agent/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agent/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Agent agent)
        {
            if (agent.Properties == null)
                agent.Properties = new List<Property>();
            if (agent.Appointments == null)
                agent.Appointments = new List<Appointment>();
            if (agent.AgentProperties == null)
                agent.AgentProperties = new List<AgentProperty>();

            if (ModelState.IsValid)
            {
                _agentService.Add(agent);
                return RedirectToAction(nameof(Index));
            }
            return View(agent);
        }

        // GET: Agent/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = _agentService.GetById(id.Value);
            if (agent == null)
            {
                return NotFound();
            }
            return View(agent);
        }

        // POST: Agent/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Agent agent)
        {
            if (id != agent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _agentService.Update(agent);
                return RedirectToAction(nameof(Index));
            }
            return View(agent);
        }

        // GET: Agent/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = _agentService.GetById(id.Value);
            if (agent == null)
            {
                return NotFound();
            }

            ViewBag.Properties = _agentService.GetPropertiesForAgent(id.Value);
            return View(agent);
        }

        // POST: Agent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _agentService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Agent/ManageProperties/5
        public IActionResult ManageProperties(Guid id)
        {
            var agent = _agentService.GetById(id);
            if (agent == null)
            {
                return NotFound();
            }

            ViewBag.Agent = agent;
            ViewBag.AssignedProperties = _agentService.GetPropertiesForAgent(id);
            ViewBag.AllProperties = new SelectList(_propertyService.GetAll(), "Id", "Title");

            return View();
        }

        // POST: Agent/AssignProperty
        [HttpPost]
        public IActionResult AssignProperty(Guid agentId, Guid propertyId)
        {
            _agentService.AssignAgentToProperty(agentId, propertyId);
            TempData["Success"] = "Property assigned to agent successfully!";
            return RedirectToAction("ManageProperties", new { id = agentId });
        }

        // POST: Agent/RemoveProperty
        [HttpPost]
        public IActionResult RemoveProperty(Guid agentId, Guid propertyId)
        {
            _agentService.RemoveAgentFromProperty(agentId, propertyId);
            TempData["Success"] = "Property removed from agent successfully!";
            return RedirectToAction("ManageProperties", new { id = agentId });
        }
    }
}
