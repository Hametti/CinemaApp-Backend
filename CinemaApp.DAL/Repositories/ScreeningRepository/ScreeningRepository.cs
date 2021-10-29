using CinemaApp.DAL.Repositories.BaseRepository;
using CinemaApp.Database;
using CinemaApp.Database.Entities.MovieModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories.ScreeningRepository
{
    public class ScreeningRepository : BaseRepository<Screening>, IScreeningRepository
    {
        public ScreeningRepository(CinemaAppDbContext cinemaAppDbContext) : base(cinemaAppDbContext)
        {

        }

        public void AddScreening(Screening screening)
        {
            _cinemaAppDbContext.Screenings.Add(screening);
            _cinemaAppDbContext.SaveChanges();
        }

        public void DeleteScreeningById(int id)
        {
            var screeningToDelete = _cinemaAppDbContext.Screenings
                .FirstOrDefault(s => s.Id == id);

            _cinemaAppDbContext.Screenings.Remove(screeningToDelete);
            _cinemaAppDbContext.SaveChanges();
        }

        public IEnumerable<Screening> GetAllScreenings()
        {
            var screenings = _cinemaAppDbContext.Screenings
                .Include(s => s.Movie)
                .ToList();

            return screenings;
        }

        public override Screening GetEntityById(int id)
        {
            var screening = _cinemaAppDbContext.Screenings
                .Include(s => s.Movie)
                .FirstOrDefault(s => s.Id == id);

            return screening;
        }
    }
}
