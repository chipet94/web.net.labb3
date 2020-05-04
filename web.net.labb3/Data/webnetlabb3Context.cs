using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web.net.labb3.Models;

namespace web.net.labb3.Data
{
    public class webnetlabb3Context : DbContext
    {
        public webnetlabb3Context (DbContextOptions<webnetlabb3Context> options)
            : base(options)
        {
        }

        public DbSet<web.net.labb3.Models.Movie> Movie { get; set; }
        public DbSet<web.net.labb3.Models.Salon> Salon { get; set; }
        public DbSet<web.net.labb3.Models.Screening> Screening { get; set; }
        public DbSet<web.net.labb3.Models.Ticket> Tickets { get; set; }
    }
}
