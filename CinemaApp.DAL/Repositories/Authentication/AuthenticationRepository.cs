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
            var user = _cinemaAppDbContext.UserCreds.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
                throw new UnauthorizedAccessException();

            return true;
        }
    }
}
