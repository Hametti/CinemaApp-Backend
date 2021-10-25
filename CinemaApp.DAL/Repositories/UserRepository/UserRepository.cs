using CinemaApp.DAL.Repositories.BaseRepository;
using CinemaApp.Database;
using CinemaApp.Database.Entities;
using CinemaApp.Database.Entities.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories.UserRepository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(CinemaAppDbContext cinemaAppDbContext) : base(cinemaAppDbContext)
        {

        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user, UserCred userCred)
        {
            _cinemaAppDbContext.Users.Add(user);
            _cinemaAppDbContext.UserCreds.Add(userCred);
            _cinemaAppDbContext.SaveChanges();
        }

        public override User GetEntityById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
