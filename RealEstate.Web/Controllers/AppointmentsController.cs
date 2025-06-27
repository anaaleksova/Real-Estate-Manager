namespace RealEstate.Web.Controllers
{
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

            public AppointmentsController(IAppointmentService appointmentService, IPropertyService propertyService)
            {
                _appointmentService = appointmentService;
                _propertyService = propertyService;

            }

            public IActionResult Index() => View(_appointmentService.GetAll());

            public IActionResult Details(Guid? id)
            {
                if (id == null) return NotFound();
                var ap = _appointmentService.GetById(id.Value);
                if (ap == null) return NotFound();
                return View(ap);
            }

            [Authorize(Roles = "Agent")]
            public IActionResult Create()
            {
                ViewBag.Properties = new SelectList(_propertyService.GetAll(), "Id", "Title");
                //ViewBag.Agents = new SelectList(_agentService.GetAll(), "Id", "FullName");
                //ViewBag.Clients = new SelectList(_clientService.GetAll(), "Id", "FullName");
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            [Authorize(Roles = "Agent")]
            public IActionResult Create([Bind("PropertyId,AgentId,ClientId,ScheduledAt,Status")] Appointment ap)
            {
                if (ModelState.IsValid)
                {
                    _appointmentService.Add(ap);
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Properties = new SelectList(_propertyService.GetAll(), "Id", "Title", ap.PropertyId);
                //ViewBag.Agents = new SelectList(_agentService.GetAll(), "Id", "FullName", ap.AgentId);
                //ViewBag.Clients = new SelectList(_clientService.GetAll(), "Id", "FullName", ap.ClientId);
                return View(ap);
            }

            public IActionResult Edit(Guid? id)
            {
                if (id == null) return NotFound();
                var ap = _appointmentService.GetById(id.Value);
                if (ap == null) return NotFound();

                ViewBag.Properties = new SelectList(_propertyService.GetAll(), "Id", "Title", ap.PropertyId);
                //ViewBag.Agents = new SelectList(_agentService.GetAll(), "Id", "FullName", ap.AgentId);
                //ViewBag.Clients = new SelectList(_clientService.GetAll(), "Id", "FullName", ap.ClientId);
                return View(ap);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(Guid id, [Bind("Id,PropertyId,AgentId,ClientId,ScheduledAt,Status")] Appointment ap)
            {
                if (id != ap.Id) return NotFound();
                if (ModelState.IsValid)
                {
                    _appointmentService.Update(ap);
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Properties = new SelectList(_propertyService.GetAll(), "Id", "Title", ap.PropertyId);
                //ViewBag.Agents = new SelectList(_agentService.GetAll(), "Id", "FullName", ap.AgentId);
                //ViewBag.Clients = new SelectList(_clientService.GetAll(), "Id", "FullName", ap.ClientId);
                return View(ap);
            }

            public IActionResult Delete(Guid? id)
            {
                if (id == null) return NotFound();
                var ap = _appointmentService.GetById(id.Value);
                if (ap == null) return NotFound();
                return View(ap);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteConfirmed(Guid id)
            {
                _appointmentService.DeleteById(id);
                return RedirectToAction(nameof(Index));
            }
        }
    }

}
