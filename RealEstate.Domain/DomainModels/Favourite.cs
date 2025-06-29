using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Domain.Identity;

namespace RealEstate.Domain.DomainModels
{
    public class Favorite : BaseEntity
    {
        public string ClientId { get; set; }
        public ApplicationUser Client { get; set; }

        public virtual ICollection<PropertyInFavorite> PropertiesInFavourite { get; set; }
    }

}
