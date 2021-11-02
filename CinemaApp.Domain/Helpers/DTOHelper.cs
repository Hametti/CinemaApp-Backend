using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Database.Entities.UserModels;
using CinemaApp.Domain.DTO;
using CinemaApp.Domain.DTO.Reservation;
using CinemaApp.Domain.DTO.ScreeningDayDTOModels;
using CinemaApp.Domain.DTO.ScreeningToDisplayDTO;
using CinemaApp.Domain.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Helpers
{
    public class DTOHelper
    {
        public static ReservationDTO ReservationToDTO(Reservation reservation)
        {
            var reservationDTO = new ReservationDTO
            {
                Id = reservation.Id,
                Date = reservation.Date,
                ReservationHour = reservation.ReservationHour,
                ScreeningId = reservation.ScreeningId,
                MovieTitle = reservation.MovieTitle,
                ReservedSeats = reservation.ReservedSeats
            };

            return reservationDTO;
        }
        public static List<ReservationDTO> ReservationsToDTOs(IEnumerable<Reservation> reservations)
        {
            var reservationDTOs = new List<ReservationDTO>();

            foreach(Reservation reservation in reservations)
                reservationDTOs.Add(ReservationToDTO(reservation));

            return reservationDTOs;
        }

        public static UserDTO UserToDTO(User user)
        {
            var userDTO = new UserDTO
            {
                Email = user.Email,
                Name = user.Name,
                Subscription = user.Subscription,
                UniqueDiscount = user.UniqueDiscount,
                UniqueDiscountValue = user.UniqueDiscountValue,
                Reservations = ReservationsToDTOs(user.Reservations)
            };

            return userDTO;
        }

        public static List<UserDTO> UsersToDTOs(IEnumerable<User> users)
        {
            var userDTOs = new List<UserDTO>();

            foreach (User user in users)
                userDTOs.Add(UserToDTO(user));

            return userDTOs;
        }

        public static ScreeningDTO ScreeningToDTO(Screening screening)
        {
            var screeningDTO = new ScreeningDTO
            {
                Id = screening.Id,
                Movie = screening.Movie,
                Hour = screening.Hour,
                Seats = SeatsToDTOs(screening.Seats),
                ScreeningDayId = screening.ScreeningDayId
            };

            return screeningDTO;
        }
        public static List<ScreeningDTO> ScreeningsToDTOs(IEnumerable<Screening> screenings)
        {
            var screeningDTOs = new List<ScreeningDTO>();

            foreach (Screening screening in screenings)
                screeningDTOs.Add(ScreeningToDTO(screening));

            return screeningDTOs;
        }

        public static ScreeningDayDTO ScreeningDayToDTO(ScreeningDay screeningDay)
        {
            var screeningDayDTO = new ScreeningDayDTO
            {
                Id = screeningDay.Id,
                Date = screeningDay.Date,
                Screenings = ScreeningsToDTOs(screeningDay.Screenings)
            };

            return screeningDayDTO;
        }

        public static List<ScreeningDayDTO> ScreeningDaysToDTOs(IEnumerable<ScreeningDay> screeningDays)
        {
            var screeningDayDTOs = new List<ScreeningDayDTO>();

            foreach (ScreeningDay screeningDay in screeningDays)
                screeningDayDTOs.Add(ScreeningDayToDTO(screeningDay));

            return screeningDayDTOs;
        }

        public static SeatDTO SeatToDTO(Seat seat)
        {
            var SeatDTO = new SeatDTO
            {
                Id = seat.Id,
                Row = seat.Row,
                SeatNumber = seat.SeatNumber,
                IsOccupied = seat.IsOccupied,
                ScreeningId = seat.ScreeningId
            };

            return SeatDTO;
        }

        public static List<SeatDTO> SeatsToDTOs(IEnumerable<Seat> seats)
        {
            var seatDTOs = new List<SeatDTO>();

            foreach (Seat seat in seats)
                seatDTOs.Add(SeatToDTO(seat));

            seatDTOs = seatDTOs.OrderBy(s => s.Row).ThenBy(s => s.SeatNumber).ToList();
            return seatDTOs;
        }

        public static ScreeningDayToDisplayDTO ScreeningDayToScreeningDayToDisplayDTO(ScreeningDay screeningDay)
        {
            var sortedScreenings = screeningDay.Screenings.OrderBy(s => s.Movie.Id).ToList();
            var screeningDTOs = new List<ScreeningToDisplayDTO>();
            foreach(Screening screening in sortedScreenings)
            {
                var screeningDTO = screeningDTOs.FirstOrDefault(s => s.MovieId == screening.Movie.Id);
                if (screeningDTO == null)
                    screeningDTOs.Add(new ScreeningToDisplayDTO 
                    {
                        MovieId = screening.Movie.Id,
                        MovieTitle = screening.Movie.Title,
                        PictureUrl = screening.Movie.PictureUrl,
                        ScreeningHours = new List<ScreeningHourDTO> { new ScreeningHourDTO { ScreeningId = screening.Id, Hour = screening.Hour } }
                    });

                else
                    screeningDTO.ScreeningHours.Add(new ScreeningHourDTO { ScreeningId = screening.Id, Hour = screening.Hour });
            }

            var screeningDayToDisplayDTO = new ScreeningDayToDisplayDTO
            {
                Id = screeningDay.Id,
                Date = screeningDay.Date,
                Screenings = screeningDTOs
            };

            return screeningDayToDisplayDTO;
        }

        public static IEnumerable<ScreeningDayToDisplayDTO> ScreeningDaysToScreeningDaysToDisplayDTO(IEnumerable<ScreeningDay> screeningDays)
        {
            var screeningDaysToDisplayDTO = new List<ScreeningDayToDisplayDTO>();

            foreach(ScreeningDay screeningDay in screeningDays)
                screeningDaysToDisplayDTO.Add(ScreeningDayToScreeningDayToDisplayDTO(screeningDay));

            return screeningDaysToDisplayDTO;
        }
    }
}
