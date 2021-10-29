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
        public string Date { get; set; }
        public string ReservationHour { get; set; }
        public int ScreeningId { get; set; }
        public string MovieTitle { get; set; }
        public List<Seat> ReservedSeats { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
