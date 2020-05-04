using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using web.net.labb3.Data;
using web.net.labb3.Models;

namespace web.net.labb3.CustomHelpers
{
    /// <summary>Class <c>ContextHelper</c>
    /// DBcontext helper for labb3-context so no SQL is needed! 
    /// To enable or disable output: Change the value of Debug to true or false. 
    /// Refresh = reset date/time on all screenings. 
    /// </summary>
    ///
    public static class ContextHelper
    {
        //Change these in Startup.cs under Configurate()
        public static bool Debug { get; set; } = false;
        public static bool Refresh { get; set; } = false;

        /// <summary>method <c>Check</c> Checks if the given context contains any values and populates them if needed.
        /// If refresh is true then all screening dates updates to current time</summary>
        public static async Task Check(webnetlabb3Context _context)
        {
            _context.Database.EnsureCreated();

            PrintInfo("Running checks...");
            if (await Empty(_context.Movie))
            {
                var movies = DummyMovies();
                await _context.Movie.AddRangeAsync(movies);
                await _context.SaveChangesAsync();
            }
            PrintInfo("Movies [Done]");
            if (await Empty(_context.Salon))
            {
                await _context.Salon.AddRangeAsync(DummySalons());
                await _context.SaveChangesAsync();
            }
            PrintInfo("Salons [Done]");
            if (await Empty(_context.Screening))
            {
                var screenings = await DummyScreenings(_context);
                await _context.Screening.AddRangeAsync(screenings);
            }
            PrintInfo("Screenings [Done]");
            if (Refresh)
            {
                await RefreshScreenings(_context);
            }
            await _context.SaveChangesAsync();
            PrintInfo("Checks complete!");
        }

        private static async Task<bool> Empty<T>(DbSet<T> entity) where T : class
        {
            var result = await entity.CountAsync() > 0 == true ? false : true;
            PrintInfo($"{typeof(T)} status: " + (result == false ? "Has Data" : "Empty"));
            return result;
        }

        private static ICollection<Movie> DummyMovies()
        {
            var x = new Movie[]
                {
                    new Movie{Title="Napoleon Dynamite", Length= 95, Genre = Genre.Comedy, ReleaseDate=DateTime.Parse("June 11, 2004") },
                    new Movie{Title="Conan the Barbarian", Length= 129, Genre = Genre.Adventure, ReleaseDate=DateTime.Parse("March 31, 1982") },
                    new Movie{Title="Bill & Ted's Excellent Adventure", Length= 90, Genre = Genre.Adventure, ReleaseDate=DateTime.Parse("February 17, 1989") }
                };
            return x;
        }

        private static ICollection<Salon> DummySalons()
        {
            var x = new Salon[] {
                new Salon{Name = "Small", Seats = 50},
                new Salon{Name = "Big", Seats = 100}
            };
            return x;
        }

        private static async Task< ICollection<Screening>> DummyScreenings(webnetlabb3Context _context, int amountPerSalon = 10)
        {
            var x = new List<Screening>();
            var TimeStarted = DateTime.Now;
            var prices = new decimal[]{100, 120, 150};
            foreach (var salon in await _context.Salon.ToListAsync())
            {
                var date = TimeStarted;
                for (int i = 0; i < amountPerSalon; i++)
                {
                    var movie = await RandomMovie(_context.Movie);
                    var s = new Screening
                    {
                        Date = date,
                        SalonID = salon.SalonID,
                        MovieID = movie.MovieID,
                        Price = prices[new Random().Next(0, prices.Length)],
                        Tickets = new List<Ticket>()
                    };
                    date = s.Date.AddMinutes(movie.Length);
                    x.Add(s);
                }
            }         
            return x;
        }
        private static async Task<Movie> RandomMovie(DbSet<Movie> _context)
        {
            var rnd = new Random();
            var movies = await _context.ToArrayAsync();
            int len = movies.Length;
            return movies[rnd.Next(len)];
        }


        private static async Task RefreshScreenings(webnetlabb3Context _context)
        {
            PrintInfo("Refreshing...");
            var startTime = DateTime.Now;
            var salonList = await _context.Salon.Include(s => s.Screenings).ThenInclude(m => m.Movie).ToListAsync();
            foreach (var salon in salonList)
            {
                var date = startTime;
                foreach (var screening in salon.Screenings)
                {
                    screening.Date = date;
                    date = screening.Date.AddMinutes(screening.Movie.Length);
                }
            }
            PrintInfo("Refresh [Done]");
        }

        private static void PrintInfo(string text, string type = "info")
        {
            if (Debug)
            {
                string message = $"'[DatabaseHandler]'<{type}>': '{text}'";
                CPrinter.PrintInfo(message, ConsoleColor.Green);
            }
        }
    }


}
