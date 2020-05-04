using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using web.net.labb3.Data;
using web.net.labb3.Models;

namespace web.net.labb3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly webnetlabb3Context _context;

        public HomeController(ILogger<HomeController> logger, webnetlabb3Context context)
        {
            _logger = logger;
            _context = context;
        }
        public SalonsViewModel ViewModel { get; set; }

        public async Task<IActionResult> Index(string sortOrder)
        {
            sortOrder = sortOrder == null ? "Date" : sortOrder;
            ViewBag.TimeSortString = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.MovieSortString = sortOrder == "Title" ? "title_desc" : "Title";

            var screening = await _context.Screening
                .Include(m => m.Movie)
                .Include(m => m.Salon)
                .Include(m => m.Tickets)
                .ToListAsync();
            ViewModel = new SalonsViewModel
            {
                sortOrder = sortOrder,
                Screenings = screening
            };
            return View(ViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
