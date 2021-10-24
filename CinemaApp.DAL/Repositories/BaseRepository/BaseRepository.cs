using CinemaApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories.BaseRepository
{
    public abstract class BaseRepository<Entity> : IBaseRepository<Entity> where Entity : class
    {
        protected readonly CinemaAppDbContext _cinemaAppDbContext;
        public BaseRepository(CinemaAppDbContext cinemaAppDbContext)
        {
            cinemaAppDbContext.Database.EnsureCreated();
            _cinemaAppDbContext = cinemaAppDbContext;
        }

        public abstract Entity GetEntityById(int id);

        public void SaveChanges()
        {
            _cinemaAppDbContext.SaveChanges();
        }
    }
}
