using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITCareerProject.Data;

namespace ITCareerProject.Services.DTOs.Tickets
{
    public class TicketWithEventAndUserDto
    {
        public int Id { get; set; }
        public Event Event { get; set; }
        public ApplicationUser User { get; set; }
    }
}
