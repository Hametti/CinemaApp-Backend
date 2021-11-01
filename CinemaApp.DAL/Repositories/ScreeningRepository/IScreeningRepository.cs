using CinemaApp.DAL.Repositories.BaseRepository;
using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Database.Entities.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories.ScreeningRepository
{
    public interface IScreeningRepository : IBaseRepository<Screening>
    {
        public IEnumerable<Screening> GetAllScreenings();
        public void AddScreening(Screening screening, int screeningDayId);
        public void DeleteScreeningById(int id);
        public IEnumerable<Seat> GetAllScreeningSeats(int id);
    }
}
