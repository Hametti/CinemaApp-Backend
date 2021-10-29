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
        public User GetUserByEmail(string email)
        {
            var user = _cinemaAppDbContext.Users.Include(u => u.UniqueDiscount).FirstOrDefault(u => u.Email == email);
            return user;
        }
        public IEnumerable<User> GetAllUsers()
        {
            var users = _cinemaAppDbContext.Users
                .Include(u => u.UniqueDiscount)
                .Include(u => u.Reservations)
                .ThenInclude(r => r.ReservedSeats)
                .ToList();

            return users;
        }

        public void ChangePassword(string email, string currentPassword, string newPassword)
        {
            var user = _cinemaAppDbContext.UserCreds.FirstOrDefault(u => u.Email == email && u.Password == currentPassword);

            user.Password = newPassword;
            _cinemaAppDbContext.SaveChanges();
        }

        public void DeleteAccount(string email, string password)
        {
            var userCredToDelete = _cinemaAppDbContext.UserCreds
                .FirstOrDefault(u => u.Email == email && u.Password == password);

            var userToDelete = _cinemaAppDbContext.Users
                .Include(u => u.Reservations)
                .ThenInclude(u => u.ReservedSeats)
                .FirstOrDefault(u => u.Email == email);

            var reservations = userToDelete.Reservations.ToList();
            if (reservations.Count > 0)
                foreach (Reservation reservation in reservations)
                {
                    var screening = _cinemaAppDbContext.Screenings
                        .Include(s => s.Seats)
                        .FirstOrDefault(s => s.Id == reservation.ScreeningId);

                    foreach (Seat seat in reservation.ReservedSeats)
                        screening.Seats.FirstOrDefault(s => s.Id == seat.Id).IsOccupied = false;
                }

            _cinemaAppDbContext.UserCreds.Remove(userCredToDelete);
            _cinemaAppDbContext.Remove(userToDelete);
            _cinemaAppDbContext.SaveChanges();
        }



        public override User GetEntityById(int id)
        {
            var user = _cinemaAppDbContext.Users
                .Include(u => u.UniqueDiscount)
                .FirstOrDefault(u => u.Id == id);

            return user;
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
