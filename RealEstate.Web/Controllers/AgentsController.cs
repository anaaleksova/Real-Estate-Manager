using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Domain.DomainModels;
using RealEstate.Service.Interface;
using System.Security.Claims;

namespace RealEstate.Web.Controllers
{
    [Authorize(Roles = "Agent")]
    public class AgentController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IAgentService _agentService;
        private readonly IAppointmentService _appointmentService;

        public AgentController(
            IPropertyService propertyService,
            IAgentService agentService,
            IAppointmentService appointmentService)
        {
            _propertyService = propertyService;
            _agentService = agentService;
            _appointmentService = appointmentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyProperties()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var properties = _propertyService.GetByAgent(userId);
            return View(properties);
        }

        public IActionResult CreateProperty()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProperty(Property property)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Ensure agent profile exists
                var agent = _agentService.CreateAgentProfile(userId);

                property.AgentId = userId;
                var createdProperty = _propertyService.Add(property);

                // Assign agent to property in many-to-many relationship
                _agentService.AssignAgentToProperty(agent.Id, createdProperty.Id);

                TempData["Success"] = "Property created successfully!";
                return RedirectToAction("MyProperties");
            }

            return View(property);
        }

        public IActionResult EditProperty(Guid id)
        {
            var property = _propertyService.GetById(id);
            if (property == null) return NotFound();

            // Check if current user is the owner
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (property.AgentId != userId)
            {
                return Forbid();
            }

            return View(property);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProperty(Guid id, Property property)
        {
            if (id != property.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                property.AgentId = userId;

                _propertyService.Update(property);
                TempData["Success"] = "Property updated successfully!";
                return RedirectToAction("MyProperties");
            }

            return View(property);
        }

        public IActionResult DeleteProperty(Guid id)
        {
            var property = _propertyService.GetById(id);
            if (property == null) return NotFound();

            // Check if current user is the owner
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (property.AgentId != userId)
            {
                return Forbid();
            }

            return View(property);
        }

        [HttpPost, ActionName("DeleteProperty")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePropertyConfirmed(Guid id)
        {
            var property = _propertyService.GetById(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (property?.AgentId == userId)
            {
                _propertyService.DeleteById(id);
                TempData["Success"] = "Property deleted successfully!";
            }

            return RedirectToAction("MyProperties");
        }

        public IActionResult MyAppointments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appointments = _agentService.GetAgentAppointments(userId);
            return View(appointments);
        }

        [HttpPost]
        public IActionResult UpdateAppointmentStatus(Guid appointmentId, string status)
        {
            var appointment = _appointmentService.GetById(appointmentId);
            if (appointment != null)
            {
                appointment.Status = status;
                _appointmentService.Update(appointment);
                TempData["Success"] = "Appointment status updated!";
            }

            return RedirectToAction("MyAppointments");
        }
    }
}
