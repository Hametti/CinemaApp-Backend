using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Database.Entities.UserModels;
using CinemaApp.Domain.DTO;
using CinemaApp.Domain.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Services.UserService
{
    public interface IUserService
    {
        public void AddUser(NewUserDTO user);
        public UserDTO GetUserByToken(string jwtToken);
        public IEnumerable<User> GetAllUsers(string jwtToken);
        public void DeleteAccount(string password, string jwtToken);
        public DiscountDTO GetUserDiscount(string jwtToken);
        public void ChangePassword(string currentPassword, string newPassword, string jwtToken);
        public void SubscribeNewsletter(string jwtToken);
        public void UnsubscribeNewsletter(string jwtToken);
    }
}
