using CinemaApp.Database.Entities.UserModels;
using CinemaApp.Domain.DTO.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Services.ReservationService
{
    public interface IReservationService
    {
        public void AddReservation(string jwtToken, List<int> seatIds);
        public ReservationDTO GetReservationById(string jwtToken, int id);
        public IEnumerable<ReservationDTO> GetUserReservations(string jwtToken);
        public void DeleteReservationById(string jwtToken, int id);

        //for testing purposes only
        public IEnumerable<ReservationDTO> GetAllReservations();
    }
}
