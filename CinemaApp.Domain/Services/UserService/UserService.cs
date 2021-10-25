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

        public void AddUser(UserCredDTO user)
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
    }
}
