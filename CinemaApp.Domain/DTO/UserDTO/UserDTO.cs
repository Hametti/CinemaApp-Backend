using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Domain.DTO.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.DTO.UserDTO
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public bool Subscription { get; set; }
        public Movie UniqueDiscount{ get; set; }
        public int UniqueDiscountValue { get; set; }
        public List<ReservationDTO> Reservations { get; set; }
    }
}
