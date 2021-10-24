using CinemaApp.DAL.Repositories.BaseRepository;
using CinemaApp.Database;
using CinemaApp.Database.Entities.Movie;
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
            var screeningDayToDelete = _cinemaAppDbContext.ScreeningDays
                                       .Include(s => s.Screenings)
                                       .ThenInclude(s => s.ScreeningHours)
                                       .FirstOrDefault(s => s.Id == id);

            _cinemaAppDbContext.ScreeningDays.Remove(screeningDayToDelete);
            _cinemaAppDbContext.SaveChanges();
        }

        public IEnumerable<ScreeningDay> GetAllScreeningDays()
        {
            var screeningDays = _cinemaAppDbContext.ScreeningDays
                                .Include(s => s.Screenings)
                                .ThenInclude(s => s.Movie)
                                .Include(s => s.Screenings)
                                .ThenInclude(s => s.ScreeningHours.OrderBy(h => h.Hour))
                                .ToList();

            return screeningDays;
        }

        public override ScreeningDay GetEntityById(int id)
        {
            var screeningDay = _cinemaAppDbContext.ScreeningDays
                                .Include(s => s.Screenings)
                                .ThenInclude(s => s.Movie)
                                .Include(s => s.Screenings)
                                .ThenInclude(s => s.ScreeningHours)
                                .FirstOrDefault(s => s.Id == id);

            return screeningDay;
        }
    }
}
