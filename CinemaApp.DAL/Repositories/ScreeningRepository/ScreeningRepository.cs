using CinemaApp.DAL.Repositories.BaseRepository;
using CinemaApp.Database;
using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Database.Entities.UserModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories.ScreeningRepository
{
    public class ScreeningRepository : BaseRepository<Screening>, IScreeningRepository
    {
        public ScreeningRepository(CinemaAppDbContext cinemaAppDbContext) : base(cinemaAppDbContext)
        {

        }
        public override Screening GetEntityById(int id)
        {
            var screening = _cinemaAppDbContext.Screenings
                .Include(s => s.ScreeningDay)
                .Include(s => s.Movie)
                .Include(s => s.Seats)
                .FirstOrDefault(s => s.Id == id);

            return screening;
        }

        public IEnumerable<Screening> GetAllScreenings()
        {
            var screenings = _cinemaAppDbContext.Screenings
                .Include(s => s.ScreeningDay)
                .Include(s => s.Movie)
                .Include(s => s.Seats)
                .ToList();

            return screenings;
        }

        public void AddScreening(Screening screening, int screeningDayId)
        {
            var screenings = _cinemaAppDbContext.Screenings.ToList();
            var screeningDay = _cinemaAppDbContext.ScreeningDays
                .Include(s => s.Screenings).
                FirstOrDefault(s => s.Id == screeningDayId);

            screeningDay.Screenings.Add(screening);
            _cinemaAppDbContext.SaveChanges();
        }

        public void DeleteScreeningById(int id)
        {
            var screeningToDelete = _cinemaAppDbContext.Screenings
                .FirstOrDefault(s => s.Id == id);

            var seats = _cinemaAppDbContext.Seats.ToList();

            _cinemaAppDbContext.Screenings.Remove(screeningToDelete);
            _cinemaAppDbContext.SaveChanges();
        }

        public IEnumerable<Seat> GetAllScreeningSeats(int id)
        {
            var screening = _cinemaAppDbContext.Screenings.Include(s => s.Seats).FirstOrDefault(s => s.Id == id);
            return screening.Seats;
        }
    }
}
