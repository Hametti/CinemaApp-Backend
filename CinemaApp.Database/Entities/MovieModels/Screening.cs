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
        public string Hour { get; set; }
        public List<Seat> Seats { get; set; } = new List<Seat>();
        public int ScreeningDayId { get; set; }
        public ScreeningDay ScreeningDay { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
