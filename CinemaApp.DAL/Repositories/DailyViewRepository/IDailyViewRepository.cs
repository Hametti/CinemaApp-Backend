using CinemaApp.DAL.Repositories.BaseRepository;
using CinemaApp.Database.Entities.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories.DailyViewRepository
{
    public interface IDailyViewRepository : IBaseRepository<DailyView>
    {
        public IEnumerable<DailyView> GetDailyViews();
        public void AddDailyView(DailyView dailyViewToAdd);
        public void DeleteDailyViewById(int id);
        public void DeleteAllDailyViews();
    }
}
