using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCareerProject.Data
{
    public class Event
    {
        public Event()
        {
            this.Tickets = new List<Ticket>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }

        [DataType(DataType.Date)]
        public DateTime PremiereDate { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
