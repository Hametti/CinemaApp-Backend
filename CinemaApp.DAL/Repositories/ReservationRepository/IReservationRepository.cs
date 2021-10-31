using CinemaApp.DAL.Repositories.BaseRepository;
using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Database.Entities.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories.ReservationRepository
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        public User GetUserByEmail(string email);
        public Seat GetSeatById(int id);
        public Screening GetScreeningById(int id);
        public ScreeningDay GetScreeningDayByDate(string date);
        public Movie GetMovieByTitle(string title);
        public void AddReservationToUser(int userId, Reservation reservation);
        public void DeleteReservationById(int id);
        IEnumerable<Reservation> GetUserReservations(string email);
    }
}
