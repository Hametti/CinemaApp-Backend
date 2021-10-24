using CinemaApp.DAL.Repositories.BaseRepository;
using CinemaApp.Database;
using CinemaApp.Database.Entities.Movie;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories.DailyViewRepository
{
    public class DailyViewRepository : BaseRepository<DailyView>, IDailyViewRepository
    {
        public DailyViewRepository(CinemaAppDbContext cinemaAppDbContext) : base(cinemaAppDbContext)
        {

        }

        public void AddDailyView(DailyView dailyViewToAdd)
        {
            _cinemaAppDbContext.DailyViews.Add(dailyViewToAdd);
            _cinemaAppDbContext.SaveChanges();
        }

        public void DeleteAllDailyViews()
        {
            
        }

        public void DeleteDailyViewById(int id)
        {
            var viewToDelete = _cinemaAppDbContext.DailyViews
                                .Include(d => d.MovieList)
                                .ThenInclude(m => m.ShowingHourList)
                                .Include(d => d.MovieList)
                                .ThenInclude(m => m.DailyViewList)
                                .FirstOrDefault(d => d.Id == id);

            _cinemaAppDbContext.DailyViews.Remove(viewToDelete);
            _cinemaAppDbContext.SaveChanges();
        }

        public IEnumerable<DailyView> GetDailyViews()
        {
            var viewsToDelete = _cinemaAppDbContext.DailyViews
                                .Include(v => v.MovieList)
                                .ThenInclude(s => s.ShowingHourList)
                                .ToList();

            return viewsToDelete;
        }
    }
}
