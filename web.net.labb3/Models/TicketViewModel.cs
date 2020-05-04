using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.net.labb3.Models
{
    public class TicketViewModel
    {
        public Ticket Ticket { get; set; }
        public Screening Screening { get; set; }

        public virtual SelectList SeatsListView
        {
            get
            {
                var count = Screening.FreeSeats;
                if (count > 12)
                {
                    count = 12;
                }
                List<int> x = new List<int>();
                for (int i = 0; i < count; i++)
                {
                    x.Add(i + 1);
                }
                return new SelectList(x);
            }
        }
        public decimal GetFinalPrice(int seats)
        {
            return seats * Screening.Price;
        }
    }
}
