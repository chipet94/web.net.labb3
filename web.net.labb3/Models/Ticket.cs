using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web.net.labb3.Models
{
    public class Ticket
    {
        public int TicketID { get; set; }
        public int ScreeningID { get; set; }
        [Range(1, 12)]
        public int Seats { get; set; }
    }
}
