using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Domain.DomainModels;

namespace RealEstate.Service.Interface
{
    public interface IFavoriteService
    {
        Favorite GetFavoriteForUser(string userId);
        void AddToFavorites(Guid propertyId, string userId);
        void RemoveFromFavorites(Guid propertyId, string userId);
    }
}
