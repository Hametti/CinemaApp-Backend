using CinemaApp.DAL.Repositories;
using CinemaApp.Database.Entities.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Services
{
    public class DailyViewService : IDailyViewService
    {
        private readonly IDailyViewRepository _dailyViewRepository;
        public DailyViewService(IDailyViewRepository dailyViewRepository)
        {
            _dailyViewRepository = dailyViewRepository;
        }

        public void AddDailyView(DailyView dailyViewToAdd)
        {
            //validate data
            _dailyViewRepository.AddDailyView(dailyViewToAdd);
        }

        public void DeleteAllDailyViews()
        {
            //check for errors
            _dailyViewRepository.DeleteAllDailyViews();
        }

        public void DeleteDailyView(DailyView dailyViewToDelete)
        {
            //validate data
            _dailyViewRepository.DeleteDailyView(dailyViewToDelete);
        }

        public IEnumerable<DailyView> GetDailyViews()
        {
            //validate data
            return _dailyViewRepository.GetDailyViews();
        }
    }
}
