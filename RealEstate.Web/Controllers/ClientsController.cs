using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Domain.DomainModels;
using RealEstate.Service.Interface;
using System.Security.Claims;
using RealEstate.Service.Implementation;

namespace RealEstate.Web.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IClientService _clientService;
        private readonly IFavoriteService _favoriteService;
        private readonly IAppointmentService _appointmentService;
        private readonly IAgentService _agentService;

        public ClientsController(
            IPropertyService propertyService,
            IClientService clientService,
            IFavoriteService favoriteService,
            IAppointmentService appointmentService,
            IAgentService agentService)
        {
            _propertyService = propertyService;
            _clientService = clientService;
            _favoriteService = favoriteService;
            _appointmentService = appointmentService;
            _agentService = agentService;
        }

        public IActionResult Properties()
        {
            var properties = _propertyService.GetAll();
            return View(properties);
        }

        public IActionResult PropertyDetails(Guid id)
        {
            var property = _propertyService.GetById(id);
            if (property == null) return NotFound();

            // Get agents assigned to this property
            var agents = _agentService.GetAgentsForProperty(id);
            ViewBag.Agents = agents;

            return View(property);
        }

        public IActionResult Favorites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favorite = _favoriteService.GetFavoriteForUser(userId);
            return View(favorite);
        }

        [HttpPost]
        public IActionResult AddToFavorites(Guid propertyId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _favoriteService.AddToFavorites(propertyId, userId);
            TempData["Success"] = "Property added to favorites!";
            return RedirectToAction("Properties");
        }

        [HttpPost]
        public IActionResult RemoveFromFavorites(Guid propertyId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _favoriteService.RemoveFromFavorites(propertyId, userId);
            TempData["Success"] = "Property removed from favorites!";
            return RedirectToAction("Favorites");
        }

       

        public IActionResult CreateAppointment(Guid propertyId)
        {
            var property = _propertyService.GetById(propertyId);
            if (property == null) return NotFound();

            // Get agents assigned to this property
            var agents = _agentService.GetAgentsForProperty(propertyId);
            ViewBag.Agents = agents;
            ViewBag.Property = property;

            return View(new Appointment { PropertyId = propertyId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAppointment(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Ensure client profile exists
                _clientService.CreateClientProfile(userId);

                appointment.ClientId = userId;
                _appointmentService.Add(appointment);

                TempData["Success"] = "Appointment created successfully!";
                return RedirectToAction("MyAppointments");
            }

            // If model is invalid, reload the data
            var property = _propertyService.GetById(appointment.PropertyId);
            var agents = _agentService.GetAgentsForProperty(appointment.PropertyId);
            ViewBag.Agents = agents;
            ViewBag.Property = property;

            return View(appointment);
        }

        public IActionResult MyAppointments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appointments = _clientService.GetClientAppointments(userId);
            return View(appointments);
        }
    }
}
