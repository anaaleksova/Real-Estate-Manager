using System.Security.Claims;
using RealEstate.Domain.DomainModels;
using RealEstate.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RealEstate.Web.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPropertyService _propertyService;
        private readonly IAgentService _agentService;

        public AppointmentsController(
            IAppointmentService appointmentService,
            IPropertyService propertyService,
            IAgentService agentService)
        {
            _appointmentService = appointmentService;
            _propertyService = propertyService;
            _agentService = agentService;
        }

        // GET: Appointments (Admin view - all appointments)
        public IActionResult Index()
        {
            var appointments = _appointmentService.GetAll();
            return View(appointments);
        }

        // GET: My Appointments (User view - their appointments only)
        [Authorize]
        public IActionResult MyAppointments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appointments = _appointmentService.GetUserAppointments(userId);
            return View(appointments);
        }

        // GET: Appointments/Details/5
        public IActionResult Details(Guid id)
        {
            var appointment = _appointmentService.GetById(id);
            if (appointment == null) return NotFound();
            return View(appointment);
        }

        // GET: Appointments/Create (Admin)
        public IActionResult Create()
        {
            ViewBag.Properties = new SelectList(_propertyService.GetAll(), "Id", "Title");
            ViewBag.Agents = new SelectList(_agentService.GetAll(), "Id", "Name");
            return View();
        }

        // GET: Schedule Appointment (Client - from property page)
        [Authorize]
        public IActionResult Schedule(Guid propertyId)
        {
            var property = _propertyService.GetById(propertyId);
            if (property == null) return NotFound();

            // Get agents assigned to this property
            var agents = _agentService.GetAgentsForProperty(propertyId);
            ViewBag.Agents = agents;
            ViewBag.Property = property;
            ViewBag.PropertyId = propertyId; // Pass PropertyId directly

            return View();
        }

        // POST: Schedule Appointment (Client)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Schedule(Appointment appointment)
        {
            try
            {
                // Get form values directly from Request.Form
                var propertyIdStr = Request.Form["PropertyId"];
                var agentIdStr = Request.Form["AgentId"];
                var scheduledDateStr = Request.Form["ScheduledDate"];

                // Validate and parse
                if (!Guid.TryParse(propertyIdStr, out Guid propertyId))
                {
                    TempData["Error"] = "Invalid property.";
                    return RedirectToAction("Index", "Properties");
                }

                if (!Guid.TryParse(agentIdStr, out Guid agentId) || agentId == Guid.Empty)
                {
                    TempData["Error"] = "Please select an agent.";
                    return RedirectToAction("BookAppointment", new { propertyId = propertyId });
                }

                if (!DateTime.TryParse(scheduledDateStr, out DateTime scheduledDate))
                {
                    TempData["Error"] = "Please select a valid date and time.";
                    return RedirectToAction("BookAppointment", new { propertyId = propertyId });
                }

                // Get current user
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    TempData["Error"] = "Please log in to book an appointment.";
                    return RedirectToAction("BookAppointment", new { propertyId = propertyId });
                }

                // Create appointment manually
                var appointment1 = new Appointment
                {
                    Id = Guid.NewGuid(),
                    PropertyId = propertyId,
                    ClientId = userId,
                    AgentId = agentId,
                    ScheduledDate = scheduledDate,
                    Status = "Scheduled"
                };

                // Save appointment
                _appointmentService.Add(appointment1);

                TempData["Success"] = "Appointment booked successfully!";
                return RedirectToAction("MyAppointments");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error booking appointment: {ex.Message}";
                return RedirectToAction("Index", "Properties");
            }
        }

        // POST: Appointments/Create (Admin)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _appointmentService.Add(appointment);
                TempData["Success"] = "Appointment created successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Properties = new SelectList(_propertyService.GetAll(), "Id", "Title", appointment.PropertyId);
            ViewBag.Agents = new SelectList(_agentService.GetAll(), "Id", "Name", appointment.AgentId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public IActionResult Edit(Guid id)
        {
            var appointment = _appointmentService.GetById(id);
            if (appointment == null) return NotFound();

            ViewBag.Properties = new SelectList(_propertyService.GetAll(), "Id", "Title", appointment.PropertyId);
            ViewBag.Agents = new SelectList(_agentService.GetAll(), "Id", "Name", appointment.AgentId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Appointment appointment)
        {
            if (id != appointment.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _appointmentService.Update(appointment);
                TempData["Success"] = "Appointment updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Properties = new SelectList(_propertyService.GetAll(), "Id", "Title", appointment.PropertyId);
            ViewBag.Agents = new SelectList(_agentService.GetAll(), "Id", "Name", appointment.AgentId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public IActionResult Delete(Guid id)
        {
            var appointment = _appointmentService.GetById(id);
            if (appointment == null) return NotFound();
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _appointmentService.DeleteById(id);
            TempData["Success"] = "Appointment deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}