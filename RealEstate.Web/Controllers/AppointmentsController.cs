using System.Security.Claims;
using RealEstate.Domain.DomainModels;
using RealEstate.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Domain.DTO;
using Stripe;

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

        // GET: Appointments
        public IActionResult Index()
        {
            var appointments = _appointmentService.GetAll();
            var now = DateTime.Now;

            foreach (var appointment in appointments)
            {
                if (appointment.Status != "Completed" && appointment.ScheduledDate <= now)
                {
                    appointment.Status = "Completed";
                    _appointmentService.Update(appointment);
                }
            }
            appointments = _appointmentService.GetAll();
            return View(appointments);
        }

        // GET: My Appointments
        [Authorize]
        public IActionResult MyAppointments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appointments = _appointmentService.GetUserAppointments(userId);

            var now = TimeZoneInfo.ConvertTimeFromUtc(
            DateTime.UtcNow,
            TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time")
);

            foreach (var appointment in appointments)
            {
                if (appointment.Status != "Completed" && appointment.ScheduledDate <= now)
                {
                    appointment.Status = "Completed";
                    _appointmentService.Update(appointment);
                }
            }
            appointments = _appointmentService.GetUserAppointments(userId);
            return View(appointments);
        }

        // GET: Appointments/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var appointment = _appointmentService.GetById(id.Value);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewBag.Properties = new SelectList(_propertyService.GetAll(), "Id", "Title");
            ViewBag.Agents = new SelectList(_agentService.GetAll(), "Id", "Name");
            return View();
        }

        // GET: Schedule Appointment
        [Authorize]
        public IActionResult Schedule(Guid propertyId)
        {
            var property = _propertyService.GetById(propertyId);
            if (property == null)
            {
                return NotFound();
            }

            var agents = _agentService.GetAgentsForProperty(propertyId);
            ViewBag.Agents = agents;
            ViewBag.Property = property;
            ViewBag.PropertyId = propertyId;

            return View();
        }

        // POST: Schedule Appointment (Client)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Schedule(Appointment appointment)
        {
            var propertyIdStr = Request.Form["PropertyId"];
            var agentIdStr = Request.Form["AgentId"];
            var scheduledDateStr = Request.Form["ScheduledDate"];

            if (!Guid.TryParse(propertyIdStr, out Guid propertyId) ||
                !Guid.TryParse(agentIdStr, out Guid agentId) ||
                !DateTime.TryParse(scheduledDateStr, out DateTime scheduledDate))
            {
                return BadRequest("Invalid input.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var appointment1 = new Appointment
            {
                Id = Guid.NewGuid(),
                PropertyId = propertyId,
                ClientId = userId,
                AgentId = agentId,
                ScheduledDate = scheduledDate,
                Status = "Scheduled"
            };

            _appointmentService.Add(appointment1);
            return RedirectToAction("MyAppointments");
        }

        // POST: Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _appointmentService.Add(appointment);
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
            if (appointment == null)
            {
                return NotFound();
            }
            var agents = _agentService.GetAgentsForProperty(appointment.PropertyId);
            ViewBag.Agents = agents;
            ViewBag.Properties = new SelectList(_propertyService.GetAll(), "Id", "Title", appointment.PropertyId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, EditAppointmentDTO appointment)
        {
            if (id != appointment.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var appointment1 = _appointmentService.GetById(id);
                appointment1.PropertyId = appointment.PropertyId;
                appointment1.AgentId = appointment.AgentId;
                appointment1.ScheduledDate = appointment.ScheduledDate;
                _appointmentService.Update(appointment1);
                return RedirectToAction(nameof(MyAppointments));
            }

            ViewBag.Properties = new SelectList(_propertyService.GetAll(), "Id", "Title", appointment.PropertyId);
            ViewBag.Agents = new SelectList(_agentService.GetAll(), "Id", "Name", appointment.AgentId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public IActionResult Delete(Guid id)
        {
            var appointment = _appointmentService.GetById(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _appointmentService.DeleteById(id);
            return RedirectToAction(nameof(MyAppointments));
        }

        // GET: Appointments/Cancel/5
        public IActionResult Cancel(Guid id)
        {
            var appointment = _appointmentService.GetById(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Cancel/5
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public IActionResult CancelConfirmed(Guid id)
        {
            _appointmentService.Cancel(id);
            return RedirectToAction(nameof(MyAppointments));
        }
        public IActionResult Buy(Guid id)
        {
            var property = _propertyService.GetById(id);
            if (property == null) return NotFound();

            return View(property); 
        }

        [HttpPost]
        public IActionResult CreateCheckoutSession(Guid propertyId)
        {
            Stripe.StripeConfiguration.ApiKey = "<secret_key>";

            var property = _propertyService.GetById(propertyId);
            if (property == null)
                return NotFound();

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<Stripe.Checkout.SessionLineItemOptions>
        {
            new Stripe.Checkout.SessionLineItemOptions
            {
                PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions
                {
                    Currency = "usd",
                    UnitAmount = (long)(property.Price * 100),
                    ProductData = new Stripe.Checkout.SessionLineItemPriceDataProductDataOptions
                    {
                        Name = property.Title
                    }
                },
                Quantity = 1
            }
        },
                Mode = "payment",
                SuccessUrl = Url.Action("SuccessPayment", "Appointments", new { propertyId = propertyId }, Request.Scheme),
                CancelUrl = Url.Action("CancelPayment", "Appointments", null, Request.Scheme),
            };

            var service = new Stripe.Checkout.SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }

        public IActionResult SuccessPayment(Guid propertyId)
        {
            var property = _propertyService.GetById(propertyId);
            if (property != null)
            {
                property.Status = "Sold";
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                property.BuyerId = userId;
                _propertyService.Update(property);
            }
           
            return View();
        }

    }
}