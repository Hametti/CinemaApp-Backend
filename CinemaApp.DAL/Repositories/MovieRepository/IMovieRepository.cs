using CinemaApp.DAL.Repositories.BaseRepository;
using CinemaApp.Database.Entities.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories.MovieRepository
{
    public interface IMovieRepository : IBaseRepository<Movie>
    {
        public Movie GetEntityById(int id);
        public IEnumerable<Movie> GetAllMovies();
        public void AddMovie(Movie movie);
        public void DeleteMovieById(int id);
    }
}
