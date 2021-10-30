using CinemaApp.DAL.Repositories.BaseRepository;
using CinemaApp.Database;
using CinemaApp.Database.Entities;
using CinemaApp.Database.Entities.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories.Authentication
{
    public class AuthenticationRepository :  IAuthenticationRepository
    {
        protected readonly CinemaAppDbContext _cinemaAppDbContext;
        public AuthenticationRepository(CinemaAppDbContext cinemaAppDbContext)
        {
            _cinemaAppDbContext = cinemaAppDbContext;
        }

        public bool AreUserCredsCorrect(string email, string password)
        {
            if(_cinemaAppDbContext.UserCreds.ToList().Count == 0)
            {
                var userToAdd = new User { Email = "admin", Name = "Jan", SecurityQuestion = "2+2?", SecurityQuestionAnswer = "4", Subscription = false, UniqueDiscount = _cinemaAppDbContext.Movies.FirstOrDefault() };
                var userCredsToAdd = new UserCred { Email = "admin", Password = "admin", User = userToAdd };
                _cinemaAppDbContext.Users.Add(userToAdd);
                _cinemaAppDbContext.UserCreds.Add(userCredsToAdd);
                _cinemaAppDbContext.SaveChanges();
            }

            var user = _cinemaAppDbContext.UserCreds.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
                return true;
            
            else
                return false;
        }
    }
}
