using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Service.Interface;

namespace RealEstate.Web.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IClientService _clientService;

        public FavoritesController(IFavoriteService favoriteService, IClientService clientService)
        {
            _favoriteService = favoriteService;
            _clientService = clientService;
        }

        // GET: My Favorites
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favorite = _favoriteService.GetFavoriteForUser(userId);
            return View(favorite);
        }

        // POST: Add to Favorites
        [HttpPost]
        public IActionResult Add(Guid propertyId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Ensure client profile exists
            _clientService.CreateClientProfile(userId);

            _favoriteService.AddToFavorites(propertyId, userId);
            TempData["Success"] = "Property added to favorites!";

            return RedirectToAction("Browse", "Properties");
        }

        // POST: Remove from Favorites
        [HttpPost]
        public IActionResult Remove(Guid propertyId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _favoriteService.RemoveFromFavorites(propertyId, userId);
            TempData["Success"] = "Property removed from favorites!";
            return RedirectToAction("Index");
        }
    }
}
