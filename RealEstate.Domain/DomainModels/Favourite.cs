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
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public virtual ICollection<PropertyInFavorite> Properties { get; set; }
    }

}
