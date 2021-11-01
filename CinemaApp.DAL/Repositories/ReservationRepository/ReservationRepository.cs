using CinemaApp.DAL.Repositories.BaseRepository;
using CinemaApp.Database;
using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Database.Entities.UserModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories.ReservationRepository
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(CinemaAppDbContext cinemaAppDbContext) : base(cinemaAppDbContext)
        {

        }

        public override Reservation GetEntityById(int id)
        {
            var reservation = _cinemaAppDbContext.Reservations
                .Include(r => r.ReservedSeats)
                .FirstOrDefault(r => r.Id == id);

            return reservation;
        }

        public User GetUserByEmail(string email)
        {
            var user = _cinemaAppDbContext.Users
                .Include(u => u.Reservations)
                .ThenInclude(r => r.ReservedSeats)
                .FirstOrDefault(u => u.Email == email);

            return user;
        }

        public Seat GetSeatById(int id)
        {
            var seat = _cinemaAppDbContext.Seats.FirstOrDefault(s => s.Id == id);
            return seat;
        }

        public Screening GetScreeningById(int id)
        {
            var screening = _cinemaAppDbContext.Screenings
                .Include(s => s.ScreeningDay)
                .Include(s => s.Movie)
                .FirstOrDefault(s => s.Id == id);
            return screening;
        }

        public ScreeningDay GetScreeningDayByDate(string date)
        {
            var screeningDay = _cinemaAppDbContext.ScreeningDays.FirstOrDefault(s => s.Date == date);
            return screeningDay;
        }

        public Movie GetMovieByTitle(string title)
        {
            var movie = _cinemaAppDbContext.Movies.FirstOrDefault(m => m.Title == title);
            return movie;
        }

        public void AddReservationToUser(int userId, Reservation reservation)
        {
            var user = _cinemaAppDbContext.Users
                .Include(u => u.Reservations)
                .FirstOrDefault(u => u.Id == userId);

            var reservations = _cinemaAppDbContext.Reservations
                .Include(r => r.ReservedSeats)
                .ToList();

            var screening = _cinemaAppDbContext.Screenings
                .Include(s => s.Seats)
                .FirstOrDefault(s => s.Id == reservation.ScreeningId);

            foreach(Seat seat in reservation.ReservedSeats)
                screening.Seats.FirstOrDefault(s => s.Id == seat.Id).IsOccupied = true;

            user.Reservations.Add(reservation);
            _cinemaAppDbContext.SaveChanges();
        }

        public void DeleteReservationById(int id)
        {
            var reservation = _cinemaAppDbContext.Reservations.Include(r => r.ReservedSeats).FirstOrDefault(r => r.Id == id);
            var seats = _cinemaAppDbContext.Seats.ToList();

            foreach (Seat seat in reservation.ReservedSeats)
            {
                var seatToUpdate = _cinemaAppDbContext.Seats.FirstOrDefault(s => s.Id == seat.Id);
                seatToUpdate.IsOccupied = false;
            }

            _cinemaAppDbContext.Remove(reservation);
            _cinemaAppDbContext.SaveChanges();
        }

        public IEnumerable<Reservation> GetUserReservations(string email)
        {
            var user = _cinemaAppDbContext.Users
                .Include(u => u.Reservations)
                .FirstOrDefault(u => u.Email == email);

            return user.Reservations.ToList();
        }
        
        //Only for testing purposes
        public IEnumerable<Reservation> GetAllReservations()
        {
            var reservations = _cinemaAppDbContext.Reservations.Include(r => r.ReservedSeats);
            return reservations;
        }
    }
}
