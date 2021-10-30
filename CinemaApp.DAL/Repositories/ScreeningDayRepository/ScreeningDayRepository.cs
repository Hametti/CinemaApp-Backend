using CinemaApp.DAL.Repositories.BaseRepository;
using CinemaApp.Database;
using CinemaApp.Database.Entities.MovieModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories.ScreeningDayRepository
{
    public class ScreeningDayRepository : BaseRepository<ScreeningDay>, IScreeningDayRepository
    {
        public ScreeningDayRepository(CinemaAppDbContext cinemaAppDbContext) : base(cinemaAppDbContext)
        {

        }

        public void AddScreeningDay(ScreeningDay screeningDay)
        {
            _cinemaAppDbContext.ScreeningDays.Add(screeningDay);
            _cinemaAppDbContext.SaveChanges();
        }

        public void DeleteScreeningDayById(int id)
        {
            var screenings = _cinemaAppDbContext.Screenings.ToList();
            var seats = _cinemaAppDbContext.Seats.ToList();
            var screeningDayToDelete = _cinemaAppDbContext.ScreeningDays
                                       .Include(s => s.Screenings)
                                       .ThenInclude(s => s.Seats)
                                       .FirstOrDefault(s => s.Id == id);

            _cinemaAppDbContext.Remove(screeningDayToDelete);
            _cinemaAppDbContext.SaveChanges();
        }

        public IEnumerable<ScreeningDay> GetAllScreeningDays()
        {
            var screeningDays = _cinemaAppDbContext.ScreeningDays
                                .Include(s => s.Screenings)
                                .ThenInclude(s => s.Movie)
                                .Include(s => s.Screenings)
                                .ThenInclude(s => s.Seats)
                                .ToList();

            return screeningDays;
        }

        public override ScreeningDay GetEntityById(int id)
        {
            var screeningDay = _cinemaAppDbContext.ScreeningDays
                                .Include(s => s.Screenings)
                                .ThenInclude(s => s.Movie)
                                .Include(s => s.Screenings)
                                .ThenInclude(s => s.Seats)
                                .FirstOrDefault(s => s.Id == id);

            return screeningDay;
        }
    }
}
