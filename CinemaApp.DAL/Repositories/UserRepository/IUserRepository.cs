using CinemaApp.DAL.Repositories.BaseRepository;
using CinemaApp.Database.Entities;
using CinemaApp.Database.Entities.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories.UserRepository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public void AddUser(User user, UserCred userCred);
        User GetUserByEmail(string email);
        void SubscribeNewsletter(User user);
        void UnsubscribeNewsletter(User user);
    }
}
