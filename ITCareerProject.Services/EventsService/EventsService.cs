using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ITCareerProject.Data;
using ITCareerProject.Services.DTOs.Events;
using ITCareerProject.Services.DTOs.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ITCareerProject.Services.EventsService
{
    public class EventsService : IEventsService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;


        public EventsService(ApplicationDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ICollection<EventDto>> GetAllAsync()
        {
            var events = await _dbContext
                .Events
                .Include(e => e.Tickets)
                .AsNoTracking()
                .ToListAsync();

            var eventDtos = _mapper.Map<List<EventDto>>(events);
            return eventDtos;
        }

        public async Task<ICollection<EventDto>> GetFilteredEvents(string keyword)
        {
            var events = await GetAllAsync();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                events = events
                    .Where(e => e.Name.Contains(keyword, StringComparison.InvariantCultureIgnoreCase)
                                || e.Description.Contains(keyword, StringComparison.InvariantCultureIgnoreCase))
                    .ToArray();
            }

            return events.ToList();
        }

        public async Task<EventDto?> GetByIdAsync(int eventId)
        {
            var domainEvent = await _dbContext
                .Events
                .Include(e => e.Tickets)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            var mappedEvent = _mapper.Map<EventDto>(domainEvent);
            return mappedEvent;
        }

        public async Task CreateAsync(CreateEventDto eventDto)
        {
            var newEvent = _mapper.Map<Event>(eventDto);
            newEvent.Photo = ConvertImageToBase64(eventDto.Photo);

            _dbContext.Events.Add(newEvent);

            await _dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(EventDto eventDto)
        {
            var domainEvent = await _dbContext
                .Events
                .FirstOrDefaultAsync(e => e.Id == eventDto.Id);

            if (domainEvent == null) throw new InvalidOperationException($"Domain event with id {eventDto.Id} could not be found!");

            domainEvent.Name = eventDto.Name;
            domainEvent.Description = eventDto.Description;
            domainEvent.PremiereDate = eventDto.PremiereDate;
            domainEvent.Photo = ConvertImageToBase64(eventDto.PhotoInput);

            await _dbContext.SaveChangesAsync();
        }

        private string ConvertImageToBase64(IFormFile eventDtoPhoto)
        {
            using (var stream = new MemoryStream())
            {
                eventDtoPhoto.CopyTo(stream);
                var bytes = stream.ToArray();
                return $"data:{eventDtoPhoto.ContentType};base64,{Convert.ToBase64String(bytes)}";
            }
        }

        public async Task DeleteAsync(int eventId)
        {
            var domainEvent = await _dbContext
                .Events
                .FirstOrDefaultAsync(e => e.Id == eventId);
            if (domainEvent == null) return;

            _dbContext.Events.Remove(domainEvent);
            await _dbContext.SaveChangesAsync();
        }

        public int GetEventsCount()
        {
            return _dbContext.Events.Count();
        }
    }
}
