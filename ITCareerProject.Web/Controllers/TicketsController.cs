using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ITCareerProject.Services.DTOs.Tickets;
using ITCareerProject.Services.TicketsService;
using Microsoft.AspNetCore.Authorization;

namespace ITCareerProject.Web.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketsService _ticketsService;

        public TicketsController(ITicketsService ticketsService)
        {
            _ticketsService = ticketsService;
        }
        public IActionResult Index()
        {
            var userId = User.IsInRole("Administrator")
                ? string.Empty
                : User.FindFirstValue(ClaimTypes.NameIdentifier);

            var tickets = _ticketsService.GetAll(userId);

            return View(tickets);
        }

        [HttpPost]
        public async Task<IActionResult> BuyTicket(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _ticketsService.BuyTicket(id, userId);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveTicket(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _ticketsService.RemoveTicket(id, userId);

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AdminDeleteTicket(int id)
        {
            await _ticketsService.AdminDeleteTicket(id);

            return Ok();
        }
    }
}
