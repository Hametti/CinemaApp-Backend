using CinemaApp.DAL.Repositories.MovieRepository;
using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Domain.DTO;
using CinemaApp.Domain.DTO.UserDTO;
using CinemaApp.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Services.MovieService
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void AddMovie(Movie movie)
        {
            //check if object fields are correct
            _movieRepository.AddMovie(movie);
        }

        public void AddSampleMovies()
        {
            var movies = _movieRepository.GetAllMovies().ToList();
            if (movies.Count == 0)
                _movieRepository.AddSampleMovies();
            else
                throw new ListIsNotEmptyException();
        }

        public void DeleteMovieById(int id)
        {
            var movie = _movieRepository.GetEntityById(id);
            if (movie == null)
                throw new ItemDoesntExistException();
            else
                _movieRepository.DeleteMovieById(id);
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            var movies = _movieRepository.GetAllMovies();
            if (movies == null)
                throw new ListIsEmptyException();

            return movies;
        }

        public Movie GetEntityById(int id)
        {
            var movie = _movieRepository.GetEntityById(id);
            if (movie == null)
                throw new ItemDoesntExistException();

            return movie;
        }

        public IEnumerable<Movie> GetFivemovies()
        {
            var movies = _movieRepository.GetAllMovies().ToList();
            if (movies == null)
                throw new ListIsEmptyException();
            if (movies.Count < 5)
                throw new NotEnoughMoviesException();
            return _movieRepository.GetFiveMovies();
        }

        public Movie GetRandomMovie()
        {
            var movies = _movieRepository.GetAllMovies().ToList();
            if (movies.Count == 0)
                throw new ListIsEmptyException();

            return _movieRepository.GetRandomMovie();
        }

        public DiscountDTO GetWeeklyDiscountMovie()
        {
            var weeklyDiscount = _movieRepository.GetWeeklyDiscountMovie();
            if (weeklyDiscount == null)
            {
                ChangeWeeklyDiscount();
                weeklyDiscount = _movieRepository.GetWeeklyDiscountMovie();
            }

            var discountDTO = new DiscountDTO
            {
                DiscountMovie = weeklyDiscount.WeeklyDiscountMovie,
                DiscountValue = weeklyDiscount.WeeklyDiscountValue
            };

            return discountDTO;
        }

        public void ChangeWeeklyDiscount()
        {
            var movies = _movieRepository.GetAllMovies();
            if (movies == null)
                throw new ListIsEmptyException();

            _movieRepository.DrawNewWeeklyDiscount();
        }
    }
}
