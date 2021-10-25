using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Domain.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Services.MovieService
{
    public interface IMovieService
    {
        public Movie GetEntityById(int id);
        public IEnumerable<Movie> GetAllMovies();
        public void AddMovie(Movie movie);
        public void DeleteMovieById(int id);
        public IEnumerable<Movie> GetFivemovies();
        public Movie GetRandomMovie();
        public WeeklyDiscountMovieDTO GetWeeklyDiscountMovie();
    }
}
