namespace RealEstate.Web.Controllers
{
    using System.Security.Claims;
    using global::RealEstate.Domain.DomainModels;
    using global::RealEstate.Service.Interface;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    namespace RealEstate.Web.Controllers
    {
        public class AppointmentsController : Controller
        {
            private readonly IAppointmentService _appointmentService;
            private readonly IPropertyService _propertyService;
            private readonly IClientService _clientService;

            public AppointmentsController(
                IAppointmentService appointmentService,
                IPropertyService propertyService,
                IClientService clientService)
            {
                _appointmentService = appointmentService;
                _propertyService = propertyService;
                _clientService = clientService;
            }

            // GET: Appointments
            public IActionResult Index()
            {
                var appointments = _appointmentService.GetAll();
                return View(appointments);
            }

            // GET: Appointments/Details/5
            public IActionResult Details(Guid id)
            {
                var appointment = _appointmentService.GetById(id);
                if (appointment == null) return NotFound();
                return View(appointment);
            }

            // GET: Appointments/Create
            public IActionResult Create()
            {
                ViewBag.Properties = new SelectList(_propertyService.GetAll(), "Id", "Title");
                return View();
            }

            // POST: Appointments/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Create(Appointment appointment)
            {
                if (ModelState.IsValid)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    appointment.ClientId = userId;

                    // Ensure client profile exists
                    _clientService.CreateClientProfile(userId);

                    _appointmentService.Add(appointment);
                    TempData["Success"] = "Appointment created successfully!";
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Properties = new SelectList(_propertyService.GetAll(), "Id", "Title", appointment.PropertyId);
                return View(appointment);
            }

            // GET: Appointments/Edit/5
            public IActionResult Edit(Guid id)
            {
                var appointment = _appointmentService.GetById(id);
                if (appointment == null) return NotFound();

                ViewBag.Properties = new SelectList(_propertyService.GetAll(), "Id", "Title", appointment.PropertyId);
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

            // My Appointments (for current user)
            public IActionResult MyAppointments()
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var appointments = _clientService.GetClientAppointments(userId);
                return View(appointments);
            }

            // Quick Schedule from Property
            public IActionResult Schedule(Guid propertyId)
            {
                var property = _propertyService.GetById(propertyId);
                if (property == null) return NotFound();

                ViewBag.Property = property;
                return View(new Appointment { PropertyId = propertyId });
            }

            // POST: Quick Schedule
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Schedule(Appointment appointment)
            {
                if (ModelState.IsValid)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    // Ensure client profile exists
                    _clientService.CreateClientProfile(userId);

                    appointment.ClientId = userId;
                    _appointmentService.Add(appointment);

                    TempData["Success"] = "Appointment scheduled successfully!";
                    return RedirectToAction("MyAppointments");
                }

                var property = _propertyService.GetById(appointment.PropertyId);
                ViewBag.Property = property;
                return View(appointment);
            }
        }
    }
    }
