using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RealEstate.Domain.DomainModels;

namespace RealEstate.Domain.Identity
{

    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }

        public Agent? AgentProfile { get; set; }
        public Client? ClientProfile { get; set; }
    }
}
