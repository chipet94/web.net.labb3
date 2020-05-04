using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.net.labb3.Models
{
    public class BookingSuccessView
    {
        public Ticket Ticket { get; set; }
        public Screening Screening { get; set; }

        public decimal Price { get => Screening.Price * Ticket.Seats; }
    }
}
