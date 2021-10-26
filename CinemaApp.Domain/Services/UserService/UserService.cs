using CinemaApp.DAL.Repositories.UserRepository;
using CinemaApp.Database.Entities;
using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Database.Entities.UserModels;
using CinemaApp.Domain.DTO.UserDTO;
using CinemaApp.Domain.Services.MovieService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMovieService _movieService;
        public UserService(IUserRepository userRepository, IMovieService movieService)
        {
            _userRepository = userRepository;
            _movieService = movieService;
        }

        public void AddUser(UserDataDTO user)
        {
            //check for errors
            //convert DTO to user
            var userToAdd = new User
            {
                Email = user.Email,
                Name = user.Name,
                UniqueDiscount = _movieService.GetRandomMovie(),
                SecurityQuestion = user.SecurityQuestion,
                SecurityQuestionAnswer = user.SecurityQuestionAnswer
            };

            var userCredToAdd = new UserCred
            {
                Email = user.Email,
                Password = user.Password,
                User = userToAdd
            };

            _userRepository.AddUser(userToAdd, userCredToAdd);
        }

        public UserDTO GetUserByToken(string token)
        {
            if (token == "{}" || token == "" || token == null)
                throw new ArgumentException();
            
            var JWTtoken = new JwtSecurityToken(token);
            string email = JWTtoken.Claims.FirstOrDefault(c => c.Type == "unique_name").Value;
            var user = _userRepository.GetUserByEmail(email);
            var userToReturn = new UserDTO
            {
                Email = user.Email,
                Name = user.Name,
                Subscription = user.Subscription,
                DiscountMovie = user.UniqueDiscount
            };

            return userToReturn;
        }

        public WeeklyDiscountMovieDTO GetUserDiscount(string jwtToken)
        {
            var JWTtoken = new JwtSecurityToken(jwtToken);
            string email = JWTtoken.Claims.FirstOrDefault(c => c.Type == "unique_name").Value;
            var user = _userRepository.GetUserByEmail(email);

            var weeklyDiscountToReturn = new WeeklyDiscountMovieDTO
            {
                DiscountMovie = user.UniqueDiscount,
                DiscountValue = 50
            };

            return weeklyDiscountToReturn;
        }

        public bool ChangePassword(string currentPassword, string newPassword, string jwtToken)
        {
            var JWTtoken = new JwtSecurityToken(jwtToken);
            string email = JWTtoken.Claims.FirstOrDefault(c => c.Type == "unique_name").Value;

            if (_userRepository.IsPasswordCorrect(email, currentPassword))
            {
                var result = _userRepository.ChangeUserPassword(email, currentPassword, newPassword);
                return result;
            }
            else
                return false;
        }

        public void SubscribeNewsletter(string jwtToken)
        {
            //check for errors
            var JWTtoken = new JwtSecurityToken(jwtToken);
            string email = JWTtoken.Claims.FirstOrDefault(c => c.Type == "unique_name").Value;
            var user = _userRepository.GetUserByEmail(email);

            _userRepository.SubscribeNewsletter(user);
        }

        public void UnsubscribeNewsletter(string jwtToken)
        {
            //check for errors
            var JWTtoken = new JwtSecurityToken(jwtToken);
            string email = JWTtoken.Claims.FirstOrDefault(c => c.Type == "unique_name").Value;
            var user = _userRepository.GetUserByEmail(email);

            _userRepository.UnsubscribeNewsletter(user);
        }

        public bool DeleteAccount(string password, string jwtToken)
        {
            if (IsPasswordCorrect(password, jwtToken))
            {
                var email = GetEmailFromToken(jwtToken);
                _userRepository.DeleteAccount(email, password);
                return true;
            }
            else
                return false;
        }

        public bool IsPasswordCorrect(string password, string jwtToken)
        {
            var JWTtoken = new JwtSecurityToken(jwtToken);
            string email = JWTtoken.Claims.FirstOrDefault(c => c.Type == "unique_name").Value;

            var result = _userRepository.IsPasswordCorrect(email, password);
            return result;
        }
        private string GetEmailFromToken(string jwtToken)
        {
            var JWTtoken = new JwtSecurityToken(jwtToken);
            string email = JWTtoken.Claims.FirstOrDefault(c => c.Type == "unique_name").Value;
            return email;
        }
    }
}
