using CinemaApp.DAL.Repositories.BaseRepository;
using CinemaApp.Database;
using CinemaApp.Database.Entities.MovieModels;
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

        public void AddMovie(Movie movie)
        {
            _cinemaAppDbContext.Movies.Add(movie);
            _cinemaAppDbContext.SaveChanges();
        }

        public void DeleteMovieById(int id)
        {
            var movieToDelete = _cinemaAppDbContext.Movies.FirstOrDefault(m => m.Id == id);

            _cinemaAppDbContext.Movies.Remove(movieToDelete);
            _cinemaAppDbContext.SaveChanges();
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            var movies = _cinemaAppDbContext.Movies.ToList();
            return movies;
        }

        public override Movie GetEntityById(int id)
        {
            var movie = _cinemaAppDbContext.Movies.FirstOrDefault(m => m.Id == id);
            return movie;
        }

        public IEnumerable<Movie> GetFivemovies()
        {
            var movies = _cinemaAppDbContext.Movies.Take(5).ToList();
            return movies;
        }

        public Movie GetRandomMovie()
        {
            var movie = _cinemaAppDbContext.Movies.ToList().OrderBy(o => Guid.NewGuid()).First();
            return movie;
        }

        public WeeklyDiscountMovie GetWeeklyDiscountMovie()
        {
            var weeklyDiscount = _cinemaAppDbContext.WeeklyDiscountMovie.Include(w => w.WeeklyDiscount).FirstOrDefault();
            return weeklyDiscount;
        }
    }
}
