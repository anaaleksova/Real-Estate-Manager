using Microsoft.AspNetCore.Mvc;
using RealEstate.Domain.DomainModels;
using RealEstate.Service.Interface;
namespace RealEstate.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    namespace RealEstate.Web.Controllers
    {
        [Authorize]
        public class PropertiesController : Controller
        {
            private readonly IPropertyService _propertyService;
            private readonly IExternalPropertyService _externalPropertyService;

            public PropertiesController(IPropertyService propertyService, IExternalPropertyService externalPropertyService)
            {
                _propertyService = propertyService;
                _externalPropertyService = externalPropertyService;
            }

            public IActionResult Index()
            {
                return View(_propertyService.GetAll());
            }

            public IActionResult MyProperties()
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return View(_propertyService.GetByAgent(userId));
            }

            public IActionResult Details(Guid? id)
            {
                if (id == null) return NotFound();
                var prop = _propertyService.GetById(id.Value);
                if (prop == null) return NotFound();
                return View(prop);
            }

            [Authorize(Roles = "Agent")]
            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            [Authorize(Roles = "Agent")]
            public IActionResult Create([Bind("Title,Address,Description,Price,Status")] Property property)
            {
                if (ModelState.IsValid)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    property.AgentId = userId;
                    _propertyService.Add(property);
                    return RedirectToAction(nameof(MyProperties));
                }
                return View(property);
            }

            [Authorize(Roles = "Agent")]
            public IActionResult Edit(Guid? id)
            {
                if (id == null) return NotFound();
                var prop = _propertyService.GetById(id.Value);
                if (prop == null) return NotFound();
                return View(prop);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            [Authorize(Roles = "Agent")]
            public IActionResult Edit(Guid id, [Bind("Id,Title,Address,Description,Price,Status")] Property property)
            {
                if (id != property.Id) return NotFound();

                if (ModelState.IsValid)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    property.AgentId = userId;
                    _propertyService.Update(property);
                    return RedirectToAction(nameof(MyProperties));
                }

                return View(property);
            }

            [Authorize(Roles = "Agent")]
            public IActionResult Delete(Guid? id)
            {
                if (id == null) return NotFound();
                var prop = _propertyService.GetById(id.Value);
                if (prop == null) return NotFound();
                return View(prop);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            [Authorize(Roles = "Agent")]
            public IActionResult DeleteConfirmed(Guid id)
            {
                _propertyService.DeleteById(id);
                return RedirectToAction(nameof(MyProperties));
            }

            [Authorize(Roles = "Agent")]
            public async Task<IActionResult> FetchProperties()
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _externalPropertyService.ImportExternalProperties(userId);
                return RedirectToAction(nameof(MyProperties));
            }
        }
    }

}
