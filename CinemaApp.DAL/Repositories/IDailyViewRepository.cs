using CinemaApp.Database.Entities.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories
{
    public interface IDailyViewRepository : IBaseRepository<DailyView>
    {
        public IEnumerable<DailyView> GetDailyViews();
        public void AddDailyView(DailyView dailyViewToAdd);
        public void DeleteDailyView(DailyView dailyViewToDelete);
        public void DeleteAllDailyViews();
    }
}
