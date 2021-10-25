using CinemaApp.DAL.Repositories.ScreeningRepository;
using CinemaApp.Database.Entities.MovieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Services.ScreeningService
{
    public class ScreeningService : IScreeningService
    {
        private readonly IScreeningRepository _screeningRepository;
        public ScreeningService(IScreeningRepository screeningRepository)
        {
            _screeningRepository = screeningRepository;
        }

        public void AddScreening(Screening screening)
        {
            //check for errors
            _screeningRepository.AddScreening(screening);
        }

        public void DeleteScreeningById(int id)
        {
            //check for errors
            _screeningRepository.DeleteScreeningById(id);
        }

        public IEnumerable<Screening> GetAllScreenings()
        {
            var screenings = _screeningRepository.GetAllScreenings();
            return screenings;
        }

        public Screening GetEntityById(int id)
        {
            var screening = _screeningRepository.GetEntityById(id);
            return screening;
        }
    }
}
