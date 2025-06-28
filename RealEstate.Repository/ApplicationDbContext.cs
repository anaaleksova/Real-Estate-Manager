using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstate.Domain;
using RealEstate.Domain.DomainModels;
using RealEstate.Domain.Identity;

namespace RealEstate.Repository
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<PropertyInFavorite> PropertyInFavorites { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<EmailMessage> EmailMessages { get; set; }
    }
}
