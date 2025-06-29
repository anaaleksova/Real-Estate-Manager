using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Service.Interface;
using System.Security.Claims;

namespace RealEstate.Web.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly IFavoriteService _favoriteService;

        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
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
            _favoriteService.AddToFavorites(propertyId, userId);
            TempData["Success"] = "Property added to favorites!";

            return RedirectToAction("Index", "Properties");
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