using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.net.labb3.Models
{
    public class Salon
    {
        public int SalonID { get; set; }
        public int Seats { get; set; }
        public string Name { get; set; }

        public ICollection<Screening> Screenings { get; set; }
    }
}
