using CinemaApp.DAL.Repositories.BaseRepository;
using CinemaApp.Database;
using CinemaApp.Database.Entities;
using CinemaApp.Database.Entities.UserModels;
using Microsoft.EntityFrameworkCore;
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

        public void AddUser(User user, UserCred userCred)
        {
            _cinemaAppDbContext.Users.Add(user);
            _cinemaAppDbContext.UserCreds.Add(userCred);
            _cinemaAppDbContext.SaveChanges();
        }

        public override User GetEntityById(int id)
        {
            return _cinemaAppDbContext.Users.Include(u => u.UniqueDiscount).FirstOrDefault(u => u.Id == id);
        }

        public User GetUserByEmail(string email)
        {
            return _cinemaAppDbContext.Users.Include(u => u.UniqueDiscount).FirstOrDefault(u => u.Email == email);
        }

        public void SubscribeNewsletter(User user)
        {
            var userToChange = _cinemaAppDbContext.Users.FirstOrDefault(u => u.Id == user.Id);
            userToChange.Subscription = true;
            _cinemaAppDbContext.SaveChanges();
        }

        public void UnsubscribeNewsletter(User user)
        {
            var userToChange = _cinemaAppDbContext.Users.FirstOrDefault(u => u.Id == user.Id);
            userToChange.Subscription = false;
            _cinemaAppDbContext.SaveChanges();
        }
    }
}
