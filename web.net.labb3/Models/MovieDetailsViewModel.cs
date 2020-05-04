using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web.net.labb3.Controllers;

namespace web.net.labb3.Models
{
    public class MovieDetailsViewModel
    {
        public Movie Movie { get; set; }

        public IEnumerable<Screening> Screenings { get; set; }
        public IEnumerable<Screening> Upcoming
        {
            get
            {
                return Screenings
                    .Where(m => m.Date >= DateTime.Now).OrderBy(m => m.Date);
            }
        }
    }
}
