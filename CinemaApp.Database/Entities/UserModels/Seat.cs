using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Database.Entities.UserModels
{
    public class Seat
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int SeatNumber { get; set; }
        public bool IsOccupied { get; set; }
    }
}
