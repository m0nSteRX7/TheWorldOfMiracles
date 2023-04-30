using ITCareerProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ITCareerProject.Services.DTOs.Events
{
    public class EventDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }

        public IFormFile PhotoInput { get; set; }

        [DataType(DataType.Date)]

        public DateTime PremiereDate { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
