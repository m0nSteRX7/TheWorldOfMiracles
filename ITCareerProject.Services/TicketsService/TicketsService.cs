using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Collections.Extensions;
using ITCareerProject.Data;
using ITCareerProject.Services.DTOs.Tickets;
using Microsoft.EntityFrameworkCore;

namespace ITCareerProject.Services.TicketsService
{
    public class TicketsService : ITicketsService
    {
        private readonly ApplicationDbContext _dbContext;

        public TicketsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int GetTicketsCount()
        {
            return _dbContext.Tickets.Count();
        }

        public async Task BuyTicket(int eventId, string userId)
        {
            var ticketExists = await _dbContext.Tickets.AnyAsync(t => t.UserId == userId && t.EventId == eventId);

            if (ticketExists)
                throw new InvalidOperationException("You have already bought a ticket!");

            _dbContext.Tickets.Add(new Ticket()
            {
                EventId = eventId,
                UserId = userId
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveTicket(int eventId, string userId)
        {
            var existingTicket = await _dbContext.Tickets.FirstOrDefaultAsync(t => t.UserId == userId && t.EventId == eventId);

            if (existingTicket == null)
                throw new InvalidOperationException("You need to buy a ticket first!");

            _dbContext.Tickets.Remove(existingTicket);

            await _dbContext.SaveChangesAsync();
        }

        public async Task AdminDeleteTicket(int ticketId)
        {
            var existingTicket = await _dbContext.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);

            if (existingTicket == null)
                throw new InvalidOperationException("You need to buy a ticket first!");

            _dbContext.Tickets.Remove(existingTicket);

            await _dbContext.SaveChangesAsync();
        }

        public List<TicketWithEventAndUserDto> GetAll(string userId)
        {
            var tickets = _dbContext
                .Tickets
                .Include(t => t.ApplicationUser)
                .Include(t => t.Event)
                .WhereIf(!string.IsNullOrWhiteSpace(userId), t => t.UserId == userId)
                .ToList();

            return tickets.Select(t => new TicketWithEventAndUserDto()
            {
                Id = t.Id,
                User = t.ApplicationUser,
                Event = t.Event
            })
                .ToList();
        }
    }
}
