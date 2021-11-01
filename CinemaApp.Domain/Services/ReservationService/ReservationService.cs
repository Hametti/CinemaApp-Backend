using CinemaApp.DAL.Repositories.ReservationRepository;
using CinemaApp.Database.Entities.UserModels;
using CinemaApp.Domain.DTO.Reservation;
using CinemaApp.Domain.Exceptions;
using CinemaApp.Domain.Helpers;
using CinemaApp.Domain.Services.ScreeningService;
using CinemaApp.Domain.Services.UserService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public ReservationDTO GetReservationById(string jwtToken, int id)
        {
            //check if user is admin
            if (!IsAdmin(jwtToken))
            {
                var user = _reservationRepository.GetUserByEmail(GetEmailFromToken(jwtToken));
                var userReservation = user.Reservations.FirstOrDefault(r => r.Id == id);
                if (userReservation == null)
                    throw new UnauthorizedAccessException();
            }

            var reservation = _reservationRepository.GetEntityById(id);
            if (reservation == null)
                throw new ItemDoesntExistException();

            var reservationDTO = DTOHelper.ReservationToDTO(reservation);

            return reservationDTO;
        }
        public IEnumerable<ReservationDTO> GetUserReservations(string jwtToken)
        {
            var user = _reservationRepository.GetUserByEmail(GetEmailFromToken(jwtToken));
            if (user == null)
                throw new UnauthorizedAccessException();

            if (user.Reservations == null)
                throw new ListIsEmptyException();

            var reservationDTOs = DTOHelper.ReservationsToDTOs(user.Reservations);

            return reservationDTOs;
        }

        public void AddReservation(string jwtToken, List<int> seatIds)
        {
            var user = _reservationRepository.GetUserByEmail(GetEmailFromToken(jwtToken));
            if (user == null)
                throw new UnauthorizedAccessException();

            if (seatIds.Count == 0 || seatIds == null)
                throw new ItemDoesntExistException();

            var seats = new List<Seat>();
            foreach(int id in seatIds)
            {
                var seat = _reservationRepository.GetSeatById(id);
                if (seat == null)
                    throw new ItemDoesntExistException();

                if (seat.IsOccupied)
                    throw new SeatIsOccupiedException();

                seats.Add(seat);
            }

            var screeningId = seats[0].ScreeningId;
            foreach(Seat seat in seats)
                if (seat.ScreeningId != screeningId)
                    throw new MultipleScreeningsException();

            var screening = _reservationRepository.GetScreeningById(screeningId);
            if (screening == null)
                throw new ItemDoesntExistException();

            if (screening.ScreeningDay == null)
                throw new ItemDoesntExistException();

            if (screening.Movie == null)
                throw new ItemDoesntExistException();

            var reservation = new Reservation
            {
                Date = screening.ScreeningDay.Date,
                ReservationHour = screening.Hour,
                ScreeningId = screening.Id,
                MovieTitle = screening.Movie.Title,
                ReservedSeats = seats,
            };

            _reservationRepository.AddReservationToUser(user.Id, reservation);
        }

        public void DeleteReservationById(string jwtToken, int id)
        {            
            if(!IsAdmin(jwtToken))
            {
                var user = _reservationRepository.GetUserByEmail(GetEmailFromToken(jwtToken));
                var userReservation = user.Reservations.FirstOrDefault(r => r.Id == id);
                if (userReservation == null)
                    throw new UnauthorizedAccessException();
            }

            var reservation = _reservationRepository.GetEntityById(id);
            if (reservation == null)
                throw new ItemDoesntExistException();

            _reservationRepository.DeleteReservationById(id);
        }

        private bool IsAdmin(string jwtToken)
        {
            var JWTtoken = new JwtSecurityToken(jwtToken);
            var role = JWTtoken.Claims.FirstOrDefault(c => c.Type == "role").Value;

            if (role == "admin")
                return true;
            else 
                return false;
        }

        private string GetEmailFromToken(string jwtToken)
        {
            var JWTtoken = new JwtSecurityToken(jwtToken);
            string email = JWTtoken.Claims.FirstOrDefault(c => c.Type == "email").Value;

            return email;
        }

        //for testing purposes only
        public IEnumerable<ReservationDTO> GetAllReservations()
        {
            var reservations = _reservationRepository.GetAllReservations();
            var reservationDTOs = DTOHelper.ReservationsToDTOs(reservations);
            return reservationDTOs;
        }
    }
}
