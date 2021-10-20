using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Entities.Movie
{
    public class ShowingHour
    {
        public int ShowingHourId { get; set; }
        public string Hour { get; set; }
        public int MovieId { get; set; }
    }
}
