using CinemaApp.Data;
using CinemaApp.Data.Entities.Movie;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories
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

        public void DeleteDailyView(DailyView dailyViewToDelete)
        {
            var viewToDelete = _cinemaAppDbContext.DailyViews
                                //add include
                                .FirstOrDefault(d => d.DailyViewId == dailyViewToDelete.DailyViewId);

            _cinemaAppDbContext.DailyViews.Remove(viewToDelete);
            _cinemaAppDbContext.SaveChanges();
        }

        public IEnumerable<DailyView> GetDailyViews()
        {
            var viewsToDelete = _cinemaAppDbContext.DailyViews
                                .Include(v => v.movieList)
                                .ThenInclude(s => s.ShowingHours)
                                .ToList();

            return viewsToDelete;
        }
    }
}
