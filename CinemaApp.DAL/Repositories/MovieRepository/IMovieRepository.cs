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
        public IEnumerable<Movie> GetAllMovies();
        public void AddMovie(Movie movie);
        public void DeleteMovieById(int id);
        public IEnumerable<Movie> GetFivemovies();
        public Movie GetRandomMovie();
    }
}
