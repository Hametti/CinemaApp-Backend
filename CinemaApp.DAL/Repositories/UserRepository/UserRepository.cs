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

        public bool ChangeUserPassword(string email, string currentPassword, string newPassword)
        {
            var user = _cinemaAppDbContext.UserCreds.FirstOrDefault(u => u.Email == email && u.Password == currentPassword);
            if (user != null)
            {
                user.Password = newPassword;
                _cinemaAppDbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public override User GetEntityById(int id)
        {
            return _cinemaAppDbContext.Users.Include(u => u.UniqueDiscount).FirstOrDefault(u => u.Id == id);
        }

        public User GetUserByEmail(string email)
        {
            return _cinemaAppDbContext.Users.Include(u => u.UniqueDiscount).FirstOrDefault(u => u.Email == email);
        }

        public bool IsPasswordCorrect(string email, string password)
        {
            var userCredFromDb = _cinemaAppDbContext.UserCreds.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (userCredFromDb == null)
                return false;

            else
                return true;
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
