using ITCareerProject.Services.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITCareerProject.Services.DTOs.Events;

namespace ITCareerProject.Services.EventsService
{
    public interface IEventsService
    {
        Task<ICollection<EventDto>> GetAllAsync();
        Task<ICollection<EventDto>> GetFilteredEvents(string keyword);
        Task<EventDto?> GetByIdAsync(int eventId);
        Task CreateAsync(CreateEventDto eventDto);
        Task EditAsync(EventDto eventDto);
        Task DeleteAsync(int eventId);
        int GetEventsCount();
    }
}
