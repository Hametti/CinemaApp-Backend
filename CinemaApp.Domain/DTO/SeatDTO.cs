using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.DTO
{
    public class SeatDTO
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int SeatNumber { get; set; }
        public bool IsOccupied { get; set; }
        public int ScreeningId { get; set; }
    }
}
