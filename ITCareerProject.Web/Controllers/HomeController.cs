using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ITCareerProject.Services.EventsService;
using ITCareerProject.Services.TicketsService;
using ITCareerProject.Services.UsersService;

namespace ITCareerProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IEventsService _eventsService;
        private readonly ITicketsService _ticketsService;

        public HomeController(IUsersService usersService,
            IEventsService eventsService,
            ITicketsService ticketsService)
        {
            _usersService = usersService;
            _eventsService = eventsService;
            _ticketsService = ticketsService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Administrator"))
            {
                ViewBag.UsersCount = _usersService.GetUsersCount();
                ViewBag.EventsCount = _eventsService.GetEventsCount();
                ViewBag.TicketsCount = _ticketsService.GetTicketsCount();
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return NotFound();
        }
    }
}