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
        public User GetUserByEmail(string email);
        public IEnumerable<User> GetAllUsers();
        public void DeleteAccount(string email, string password);
        public void ChangePassword(string email, string currentPassword, string newPassword);
        public void SubscribeNewsletter(User user);
        public void UnsubscribeNewsletter(User user);
        public bool IsPasswordCorrect(string email, string password);
        public void AddSampleData();
        public void AddSampleUsers();
        public void AddSampleMovies();
        public void AddSampleScreeningDays();
        public void AddSampleReservationToDefaultUser();
    }
}
