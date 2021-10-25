using CinemaApp.Database.Entities.MovieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Services.ScreeningService
{
    public interface IScreeningService
    {
        public Screening GetEntityById(int id);
        public IEnumerable<Screening> GetAllScreenings();
        public void AddScreening(Screening screening);
        public void DeleteScreeningById(int id);
    }
}
