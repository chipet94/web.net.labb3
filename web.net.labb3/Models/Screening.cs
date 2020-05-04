using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace web.net.labb3.Models
{
    public class Screening
    {
        public int ScreeningID { get; set; }
        public int SalonID { get; set; }
        public int MovieID { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

        public virtual Salon Salon { get; set; }
        public virtual Movie Movie { get; set; }
        public ICollection<Ticket> Tickets { get; set; }

        public virtual int FreeSeats
        {
            get => Salon.Seats - Tickets.Sum(s => s.Seats);
        }
        public virtual string Time
        {
            get => Date.ToShortTimeString();
        }
    }
}
