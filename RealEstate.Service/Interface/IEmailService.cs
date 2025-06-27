using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Domain;

namespace RealEstate.Service.Interface
{
    public interface IEmailService
    {
        Boolean SendEmailAsync(EmailMessage allMails);
    }

}
