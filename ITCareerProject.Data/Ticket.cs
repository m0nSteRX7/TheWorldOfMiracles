using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCareerProject.Data
{
    public class Ticket
    {
        public int Id { get; set; }
        public string UserId { get; set; }
       
        [ForeignKey(nameof(UserId))]
        public ApplicationUser ApplicationUser { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
