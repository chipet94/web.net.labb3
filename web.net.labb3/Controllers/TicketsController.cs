using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.net.labb3.Data;
using web.net.labb3.Models;

namespace web.net.labb3.Controllers
{
    public class TicketsController : Controller
    {
        private readonly webnetlabb3Context _context;

        public TicketsController(webnetlabb3Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Success()
        {
            var tId = TempData["TID"];
            if (tId != null)
            {
                Console.WriteLine(tId);
                var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.TicketID == (int)tId);
                if (ticket == null)
                {
                    return NotFound();
                }
                var screening = await _context.Screening
                        .Include(m => m.Salon)
                        .Include(m => m.Movie)
                        .FirstOrDefaultAsync(m => m.ScreeningID == ticket.ScreeningID);
                if (screening == null)
                {
                    return NotFound();
                }
                var x = new BookingSuccessView
                {
                    Ticket = ticket,
                    Screening = screening

                };
                return View(x);
            }
            return NotFound();

        }

        // GET: Tickets/Create
        public async Task<IActionResult> Create(int? id)
        {
            var x = new TicketViewModel
            {
                Screening = await _context.Screening
                    .Include(m => m.Salon)
                    .Include(m => m.Movie)
                    .Include(m => m.Tickets)
                .FirstOrDefaultAsync(m => m.ScreeningID == id)
            };
            if (x.Screening != null && x.Screening.FreeSeats > 0)
            {
                return View(x);
            }
            return NotFound();
        }

        // POST: Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketID, ScreeningID, Seats")] Ticket ticket)
        {

            if (ModelState.IsValid)
            {
                var screening = await _context.Screening
                       .Include(m => m.Salon)
                       .Include(m => m.Tickets)
                   .FirstOrDefaultAsync(m => m.ScreeningID == ticket.ScreeningID);
                if (screening.FreeSeats >= ticket.Seats)
                {
                    _context.Add(ticket);
                    await _context.SaveChangesAsync();
                    TempData["TID"] = ticket.TicketID;
                    return RedirectToAction(nameof(Success));
                }
            }
            var x = new TicketViewModel
            {
                 Screening = await _context.Screening
                    .Include(m => m.Salon)
                    .Include(m => m.Movie)
                    .Include(m => m.Tickets)
                .FirstOrDefaultAsync(m => m.ScreeningID == ticket.ScreeningID),
                 Ticket = ticket
                
            };
            return View(x);
        }
    }
}
