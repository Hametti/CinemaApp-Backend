using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Database.Entities.UserModels;
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
        public void AddUser(UserCredDTO user);
        UserDTO GetUserByToken(string token);
        WeeklyDiscountMovieDTO GetUserDiscount(string jwtToken);
        void SubscribeNewsletter(string jwtToken);
        void UnsubscribeNewsletter(string jwtToken);
    }
}
