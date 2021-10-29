using CinemaApp.DAL.Repositories.BaseRepository;
using CinemaApp.Database.Entities.MovieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories.MovieRepository
{
    public interface IMovieRepository : IBaseRepository<Movie>
    {
        public void AddMovie(Movie movie);
        public void AddSampleMovies();
        public IEnumerable<Movie> GetAllMovies();
        public Movie GetRandomMovie();
        public WeeklyDiscount GetWeeklyDiscountMovie();
        public void DeleteMovieById(int id);
        public IEnumerable<Movie> GetFiveMovies();
        public void DrawNewWeeklyDiscount();
    }
}
