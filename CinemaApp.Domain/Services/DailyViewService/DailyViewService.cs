using CinemaApp.DAL.Repositories;
using CinemaApp.DAL.Repositories.DailyViewRepository;
using CinemaApp.Database.Entities.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Services.DailyViewService
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

        public void DeleteDailyViewById(int id)
        {
            //validate data
            _dailyViewRepository.DeleteDailyViewById(id);
        }

        public IEnumerable<DailyView> GetDailyViews()
        {
            //validate data
            return _dailyViewRepository.GetDailyViews();
        }
    }
}
