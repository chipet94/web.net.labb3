using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.net.labb3.Models
{
    public class SalonsViewModel
    {
        public IEnumerable<Screening> Screenings { get; set; }

        public string sortOrder { get; set; }

        public IEnumerable<Screening> Upcoming
        {
            get
            {
                return Screenings.Where(m => m.Date >= DateTime.Now).OrderBy(m => m.Date);
            }
        }
        public IEnumerable<Screening> UpcomingShort
        {
            get
            {
                return Upcoming.Take(5);
            }
        }
        public IEnumerable<Screening> NowShowing
        {
            get
            {
                return Screenings.Where(m => m.Date < DateTime.Now && m.Date.AddMinutes(m.Movie.Length) > DateTime.Now);
            }
        }
        public IEnumerable<Screening> SortScreenings( IEnumerable<Screening> screenings)
        {
            switch (sortOrder)
            {
                case "title_desc":
                    screenings = screenings.OrderByDescending(s => s.Movie.Title);
                    break;
                case "Title":
                    screenings = screenings.OrderBy(s => s.Movie.Title);
                    break;
                case "Date":
                    screenings = screenings.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    screenings = screenings.OrderByDescending(s => s.Date);
                    break;
                default:
                    screenings = screenings.OrderBy(s => s.Date);
                    break;
            }
            return screenings;
        }
    }
}
