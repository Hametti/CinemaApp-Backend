using CinemaApp.Database.Entities.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.DTO.Reservation
{
    public class ReservationToAddDTO
    {
        public string Date { get; set; }
        public string ReservationHour { get; set; }
        public int ScreeningId { get; set; }
        public string MovieTitle { get; set; }
        public int[] ReservedSeatsIds { get; set; }
    }
}
