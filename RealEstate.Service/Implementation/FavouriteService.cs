

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealEstate.Domain.DomainModels;
using RealEstate.Repository;
using RealEstate.Service.Interface;

namespace RealEstate.Service.Implementation
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IRepository<Favorite> _favoriteRepository;
        private readonly IRepository<PropertyInFavorite> _pifRepository;
        private readonly IRepository<Property> _propertyRepository;

        public FavoriteService(
            IRepository<Favorite> favoriteRepository,
            IRepository<PropertyInFavorite> pifRepository,
            IRepository<Property> propertyRepository)
        {
            _favoriteRepository = favoriteRepository;
            _pifRepository = pifRepository;
            _propertyRepository = propertyRepository;
        }

        public Favorite GetFavoriteForUser(string userId)
        {
            return _favoriteRepository.Get(
                selector: f => f,
                predicate: f => f.ClientId == userId,
                include: x =>
                {
                    return x.Include(y => y.PropertiesInFavourite).ThenInclude(z => z.Property);
                });
        }


        public void AddToFavorites(Guid propertyId, string userId)
        {
            var favorite = _favoriteRepository.Get(selector: x => x,
                predicate: x => x.ClientId == userId);

            if (favorite == null)
            {
                favorite = new Favorite
                {
                    Id = Guid.NewGuid(),
                    ClientId = userId,
                    PropertiesInFavourite = new List<PropertyInFavorite>()
                };
                _favoriteRepository.Insert(favorite);
            }

            var exists = _pifRepository.Get(selector: x => x,
                predicate:  p => p.PropertyId == propertyId && p.FavoriteId == favorite.Id);

            if (exists == null)
            {
                var relation = new PropertyInFavorite
                {
                    Id = Guid.NewGuid(),
                    PropertyId = propertyId,
                    FavoriteId = favorite.Id
                };
                _pifRepository.Insert(relation);
            }
        }

        public void RemoveFromFavorites(Guid propertyId, string userId)
        {

            var favorite = _favoriteRepository.Get(selector: x => x,
                predicate: x => x.ClientId == userId);

            var relation = _pifRepository.Get(selector: x => x,
                predicate: p => p.FavoriteId == favorite.Id && p.PropertyId == propertyId);

            if (relation != null)
                _pifRepository.Delete(relation);
        }

    }

}
