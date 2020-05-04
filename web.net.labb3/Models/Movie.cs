using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web.net.labb3.Models
{
    public enum Genre
    {
        Comedy, Horror, Adventure, Fantasy, Drama
    }
    public class Movie
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public Genre Genre { get; set; }
        public double Length { get; set; }
    }
}
