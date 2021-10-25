using CinemaApp.Database.Entities.MovieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Database.Entities.UserModels
{
    public class Reservation
    {
        public int Id { get; set; }
        public Screening Screening { get; set; }
        public string Date { get; set; }
        public List<Seat> Seats { get; set; }
    }
}
