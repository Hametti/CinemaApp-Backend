using CinemaApp.Data.Entities.Movie;
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
        public void AddDailyView(DailyView DailyViewToAdd);
        public void DeleteDailyView(DailyView DailyViewToDelete);
        public void DeleteAllDailyViews();
    }
}
