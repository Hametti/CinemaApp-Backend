using CinemaApp.DAL.Repositories.BaseRepository;
using CinemaApp.Database.Entities.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories.Authentication
{
    public interface IAuthenticationRepository
    {
        public bool CheckUserCreds(string username, string password);
    }
}
