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

        public void AddReservation(string jwtToken, ReservationToAddDTO reservationToAdd)
        {
            var user = _reservationRepository.GetUserByEmail(GetEmailFromToken(jwtToken));
            if (user == null)
                throw new UnauthorizedAccessException();

            List<Seat> reservedSeats = new List<Seat>();
            foreach (int seatId in reservationToAdd.ReservedSeatsIds)
            {
                var seatToReserve = _reservationRepository.GetSeatById(seatId);
                if (seatToReserve == null)
                    throw new ItemDoesntExistException();
                if (seatToReserve.IsOccupied)
                    throw new SeatIsOccupiedException();

                reservedSeats.Add(seatToReserve);
            }

            var screeningDay = _reservationRepository.GetScreeningDayByDate(reservationToAdd.Date);
            if (screeningDay == null)
                throw new ItemDoesntExistException();

            var screening = _reservationRepository.GetScreeningById(reservationToAdd.ScreeningId);
            if (screening == null && screening.Hour != reservationToAdd.ReservationHour)
                throw new ItemDoesntExistException();

            var movie = _reservationRepository.GetMovieByTitle(reservationToAdd.MovieTitle);
            if (movie == null)
                throw new ItemDoesntExistException();

            var reservation = new Reservation
            {
                Date = reservationToAdd.Date,
                ReservationHour = reservationToAdd.ReservationHour,
                ScreeningId = reservationToAdd.ScreeningId,
                MovieTitle = reservationToAdd.MovieTitle,
                ReservedSeats = reservedSeats
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
    }
}
