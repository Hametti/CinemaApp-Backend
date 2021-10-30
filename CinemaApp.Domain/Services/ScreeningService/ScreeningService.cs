using CinemaApp.DAL.Repositories.MovieRepository;
using CinemaApp.DAL.Repositories.ScreeningRepository;
using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CinemaApp.Domain.Services.ScreeningService
{
    public class ScreeningService : IScreeningService
    {
        private readonly IScreeningRepository _screeningRepository;
        private readonly IMovieRepository _movieRepository;
        public ScreeningService(IScreeningRepository screeningRepository, IMovieRepository movieRepository)
        {
            _screeningRepository = screeningRepository;
            _movieRepository = movieRepository;
        }

        public void AddScreening(Screening screening)
        {
            var screeningFromDb = _screeningRepository
                .GetAllScreenings()
                .FirstOrDefault(s => s.Hour == screening.Hour && s.ScreeningDay.Date == screening.ScreeningDay.Date);
            
            if (screeningFromDb != null)
                throw new ItemAlreadyExistsException();

            var movie = _movieRepository.GetEntityById(screening.Movie.Id);
            if (movie == null)
                throw new ItemDoesntExistException();
           
            if (screening.Hour.Length < 2 || screening.Hour.Length > 5)
                throw new ArgumentException();

            _screeningRepository.AddScreening(screening);
        }
        public IEnumerable<Screening> GetAllScreenings()
        {
            var screenings = _screeningRepository.GetAllScreenings().ToList();
            if (screenings == null && screenings.Count == 0)
                throw new ListIsEmptyException();

            return screenings;
        }

        public void DeleteScreeningById(int id)
        {
            var screening = _screeningRepository.GetEntityById(id);
            if (screening == null)
                throw new ItemDoesntExistException();

            _screeningRepository.DeleteScreeningById(id);
        }

        public Screening GetEntityById(int id)
        {
            var screening = _screeningRepository.GetEntityById(id);
            if (screening == null)
                throw new ItemDoesntExistException();

            return screening;
        }
    }
}
