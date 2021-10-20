using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;

namespace CinemaApp.Data.Entities.Movie
{
    public class DailyView
    {
        public int DailyViewId { get; set; }
        public string Date { get; set; }
        public List<Movie> movieList { get; set; }
    }
}
