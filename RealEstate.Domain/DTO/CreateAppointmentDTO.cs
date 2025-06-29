using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.DTO
{
    public class CreateAppointmentDTO
    {
        [Required]
        public Guid PropertyId { get; set; }

        [Required]
        public Guid AgentId { get; set; }

        [Required]
        public DateTime ScheduledDate { get; set; }
    }
}
