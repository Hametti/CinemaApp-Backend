using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Domain.DTO;
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
        public void AddMovie(Movie movie);
        public Movie GetEntityById(int id);
        public void AddSampleMovies();
        public IEnumerable<Movie> GetAllMovies();
        public Movie GetRandomMovie();
        public DiscountDTO GetWeeklyDiscountMovie();
        public void ChangeWeeklyDiscount();
        public void DeleteMovieById(int id);
        public IEnumerable<Movie> GetFivemovies();
    }
}
