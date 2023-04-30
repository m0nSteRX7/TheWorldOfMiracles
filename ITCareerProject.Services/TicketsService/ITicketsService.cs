using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITCareerProject.Services.DTOs.Tickets;

namespace ITCareerProject.Services.TicketsService
{
    public interface ITicketsService
    {
        int GetTicketsCount();
        Task BuyTicket(int eventId, string userId);
        Task RemoveTicket(int eventId, string userId);
        Task AdminDeleteTicket(int ticketId);
        List<TicketWithEventAndUserDto> GetAll(string userId);
    }
}
