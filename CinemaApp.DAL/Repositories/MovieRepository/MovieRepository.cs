using CinemaApp.DAL.Repositories.BaseRepository;
using CinemaApp.Database;
using CinemaApp.Database.Entities.Movie;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories.MovieRepository
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(CinemaAppDbContext cinemaAppDbContext) : base(cinemaAppDbContext)
        {

        }

        public IEnumerable<Movie> GetAllMovies()
        {
            var movies = _cinemaAppDbContext.Movies
                        .Include(m => m.DailyViewList)
                        .Include(m => m.ShowingHourList)
                        .ToList();

            return movies;
        }

        public Movie GetEntityById(int id)
        {
            var movie = _cinemaAppDbContext.Movies
                        .Include(m => m.DailyViewList)
                        .Include(m => m.ShowingHourList)
                        .FirstOrDefault(m => m.Id == id);

            return movie;
        }

        public void AddMovie(Movie movie)
        {
            _cinemaAppDbContext.Movies.Add(movie);
            _cinemaAppDbContext.SaveChanges();
        }

        public void DeleteMovieById(int id)
        {
            var movie = _cinemaAppDbContext.Movies
                        .Include(m => m.DailyViewList)
                        .Include(m => m.ShowingHourList)
                        .FirstOrDefault(m => m.Id == id);

            _cinemaAppDbContext.Movies.Remove(movie);
            _cinemaAppDbContext.SaveChanges();
        }
    }
}
