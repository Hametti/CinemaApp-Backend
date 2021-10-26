using CinemaApp.Database.Entities.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Database.Entities.MovieModels
{
    public class Screening
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public List<ScreeningHour> ScreeningHours { get; set; } = new List<ScreeningHour>();
        public List<Seat> Seats { get; set; } = new List<Seat>();
    }
}
