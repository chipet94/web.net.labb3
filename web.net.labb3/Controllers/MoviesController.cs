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
    public class MoviesController : Controller
    {
        private readonly webnetlabb3Context _context;

        public MoviesController(webnetlabb3Context context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movie.ToListAsync();

            return View(movies);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var view = new MovieDetailsViewModel
            {
                Movie = await _context.Movie
                    .FirstOrDefaultAsync(m => m.MovieID == id),
                Screenings = await _context.Screening
                    .Include(m => m.Movie)
                    .Include(m => m.Salon)
                    .Include(m => m.Tickets)
                    .Where(m => m.MovieID == id)
                    .ToListAsync()
            };
            if (view.Movie == null)
            {
                return NotFound();
            }

            return View(view);
        }
    }
}
