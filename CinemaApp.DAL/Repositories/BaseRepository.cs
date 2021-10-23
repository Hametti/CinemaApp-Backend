using CinemaApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories
{
    public abstract class BaseRepository<Entity> : IBaseRepository<Entity> where Entity : class
    {
        protected readonly CinemaAppDbContext _cinemaAppDbContext;
        public BaseRepository(CinemaAppDbContext cinemaAppDbContext)
        {
            cinemaAppDbContext.Database.EnsureCreated();
            _cinemaAppDbContext = cinemaAppDbContext;
        }
        public void SaveChanges()
        {
            _cinemaAppDbContext.SaveChanges();
        }
    }
}
