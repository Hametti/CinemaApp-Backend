using CinemaApp.DAL.Repositories.BaseRepository;
using CinemaApp.Database.Entities.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories.ScreeningDayRepository
{
    public interface IScreeningDayRepository : IBaseRepository<ScreeningDay>
    {
        public IEnumerable<ScreeningDay> GetAllScreeningDays();
        public void AddScreeningDay(ScreeningDay screeningDay);
        public void DeleteScreeningDayById(int id);
    }
}
