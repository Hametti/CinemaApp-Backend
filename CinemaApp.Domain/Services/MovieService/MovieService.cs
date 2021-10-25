using CinemaApp.DAL.Repositories.MovieRepository;
using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Domain.DTO.UserDTO;
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
            //check for errors
            _movieRepository.AddMovie(movie);
        }

        public void DeleteMovieById(int id)
        {
            //check for errors
            _movieRepository.DeleteMovieById(id);
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            var movies = _movieRepository.GetAllMovies();
            //check for errors
            return movies;
        }

        public Movie GetEntityById(int id)
        {
            var movie = _movieRepository.GetEntityById(id);
            //check for errors
            return movie;
        }

        public IEnumerable<Movie> GetFivemovies()
        {
            var movies = _movieRepository.GetFivemovies();
            return movies;
        }

        public Movie GetRandomMovie()
        {
            //check for errors
            return _movieRepository.GetRandomMovie();
        }

        public WeeklyDiscountMovieDTO GetWeeklyDiscountMovie()
        {
            //check for errors
            var weeklyDiscount = _movieRepository.GetWeeklyDiscountMovie();
            var weeklyDiscountToReturn = new WeeklyDiscountMovieDTO
            {
                DiscountMovie = weeklyDiscount.WeeklyDiscount,
                DiscountValue = weeklyDiscount.WeeklyDiscountValue
            };
            return weeklyDiscountToReturn;
        }
    }
}
