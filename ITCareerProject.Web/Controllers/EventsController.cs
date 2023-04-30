using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITCareerProject.Data;
using ITCareerProject.Services.DTOs.Events;
using ITCareerProject.Services.EventsService;
using Microsoft.AspNetCore.Authorization;
using ITCareerProject.Services.UsersService;
using ITCareerProject.Services.DTOs.Users;
using AutoMapper;

namespace ITCareerProject.Web.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEventsService _eventsService;
        private readonly IMapper _autoMapper;

        public EventsController(ApplicationDbContext context,
            IEventsService eventsService,
            IMapper autoMapper)
        {
            _context = context;
            _eventsService = eventsService;
            _autoMapper = autoMapper;
        }

        // GET: Events
        public async Task<IActionResult> Index(string keyword)
        {
            var viewModel = await _eventsService.GetFilteredEvents(keyword);

            return View(viewModel);
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domainEvent = await _eventsService.GetByIdAsync(id.Value);
            if (domainEvent == null)
            {
                return NotFound();
            }

            return View(domainEvent);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(CreateEventDto @event)
        {
            if (ModelState.IsValid)
            {
                await _eventsService.CreateAsync(@event);
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domainEvent = await _eventsService.GetByIdAsync(id.Value);
            if (domainEvent == null)
            {
                return NotFound();
            }

            return View(domainEvent);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventDto @event)
        {
            ModelState.Remove(nameof(EventDto.Photo));
            ModelState.Remove(nameof(EventDto.Tickets));

            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _eventsService.EditAsync(@event);
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domainEvent = await _eventsService.GetByIdAsync(id.Value);
            if (domainEvent == null)
            {
                return NotFound();
            }

            return View(domainEvent);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var domainEvent = await _eventsService.GetByIdAsync(id);
            if (domainEvent == null)
            {
                return NotFound();
            }

            await _eventsService.DeleteAsync(id);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
