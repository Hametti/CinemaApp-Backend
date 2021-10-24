using CinemaApp.Database.Entities.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Services.DailyViewService
{
    public interface IDailyViewService
    {
        public IEnumerable<DailyView> GetDailyViews();
        public void AddDailyView(DailyView dailyViewToAdd);
        public void DeleteDailyViewById(int id);
        public void DeleteAllDailyViews();
    }
}
