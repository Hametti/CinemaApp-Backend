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

        public void AddDailyView(DailyView DailyViewToAdd)
        {
            _cinemaAppDbContext.DailyViews.Add(DailyViewToAdd);
            _cinemaAppDbContext.SaveChanges();
        }

        public void DeleteAllDailyViews()
        {
            
        }

        public void DeleteDailyView(DailyView DailyViewToDelete)
        {
            var dailyViewToDelete = _cinemaAppDbContext.DailyViews
                                .Where(d => d.DailyViewId == DailyViewToDelete.DailyViewId)
                                .Include(v => v.movieList)
                                .ThenInclude(s => s.ShowingHours)
                                .FirstOrDefault();
            _cinemaAppDbContext.DailyViews.Remove(dailyViewToDelete);
        }

        public IEnumerable<DailyView> GetDailyViews()
        {
            throw new NotImplementedException();
        }
    }
}
