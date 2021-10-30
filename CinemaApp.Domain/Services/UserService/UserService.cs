using CinemaApp.DAL.Repositories.UserRepository;
using CinemaApp.Database.Entities;
using CinemaApp.Database.Entities.UserModels;
using CinemaApp.Domain.DTO;
using CinemaApp.Domain.DTO.UserDTO;
using CinemaApp.Domain.Exceptions;
using CinemaApp.Domain.Services.MovieService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

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

        public void AddUser(NewUserDTO userToAddDTO)
        {
            var user = _userRepository.GetUserByEmail(userToAddDTO.Email);
            if (user != null)
                throw new ItemAlreadyExistsException();

            if (userToAddDTO.Password.Length < 8)
                throw new TooShortPasswordException();

            if (userToAddDTO.Password.Length > 128)
                throw new TooLongPasswordException();

            if (userToAddDTO.Name.Length < 2)
                throw new TooShortNameException();

            if (userToAddDTO.Name.Length > 100)
                throw new TooLongNameException();

            if (userToAddDTO.SecurityQuestion.Length < 3)
                throw new TooShortSecurityQuestionException();

            if (userToAddDTO.SecurityQuestion.Length > 100)
                throw new TooLongSecurityQuestionException();

            if (userToAddDTO.SecurityQuestionAnswer.Length < 3)
                throw new TooShortSecurityQuestionAnswerException();

            if (userToAddDTO.SecurityQuestionAnswer.Length > 100)
                throw new TooLongSecurityQuestionAnswerException();

            var userToAdd = new User
            {
                Email = userToAddDTO.Email,
                Name = userToAddDTO.Name,
                UniqueDiscount = _movieService.GetRandomMovie(),
                UniqueDiscountValue = new Random().Next(10, 60),
                SecurityQuestion = userToAddDTO.SecurityQuestion,
                SecurityQuestionAnswer = userToAddDTO.SecurityQuestionAnswer
            };

            var userCredToAdd = new UserCred
            {
                Email = userToAddDTO.Email,
                Password = userToAddDTO.Password,
                User = userToAdd
            };

            _userRepository.AddUser(userToAdd, userCredToAdd);
        }

        public UserDTO GetUserByToken(string jwtToken)
        {
            if (jwtToken == "{}" || jwtToken == "" || jwtToken == null)
                throw new ItemDoesntExistException();

            var JWTtoken = new JwtSecurityToken(jwtToken);
            string email = JWTtoken.Claims.FirstOrDefault(c => c.Type == "email").Value;
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
        public IEnumerable<User> GetAllUsers(string jwtToken)
        {
            var JWTtoken = new JwtSecurityToken(jwtToken);
            var role = JWTtoken.Claims.FirstOrDefault(c => c.Type == "role").Value;
            if (role != "admin")
                throw new UnauthorizedAccessException();

            var users = _userRepository.GetAllUsers();
            if (users == null)
                throw new ListIsEmptyException();

            return users;
        }

        public void DeleteAccount(string password, string jwtToken)
        {
            var authenticationResult = IsPasswordCorrect(password, jwtToken);
            if (!authenticationResult)
                throw new UnauthorizedAccessException();
            
            var email = GetEmailFromToken(jwtToken);
            _userRepository.DeleteAccount(email, password);
        }

        public DiscountDTO GetUserDiscount(string jwtToken)
        {
            var JWTtoken = new JwtSecurityToken(jwtToken);
            string email = JWTtoken.Claims.FirstOrDefault(c => c.Type == "email").Value;

            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
                throw new ItemDoesntExistException();

            var weeklyDiscountToReturn = new DiscountDTO
            {
                DiscountMovie = user.UniqueDiscount,
                DiscountValue = user.UniqueDiscountValue
            };

            return weeklyDiscountToReturn;
        }

        public void SubscribeNewsletter(string jwtToken)
        {
            var JWTtoken = new JwtSecurityToken(jwtToken);
            string email = JWTtoken.Claims.FirstOrDefault(c => c.Type == "email").Value;

            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
                throw new ItemDoesntExistException();

            _userRepository.SubscribeNewsletter(user);
        }

        public void UnsubscribeNewsletter(string jwtToken)
        {
            var JWTtoken = new JwtSecurityToken(jwtToken);
            string email = JWTtoken.Claims.FirstOrDefault(c => c.Type == "email").Value;

            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
                throw new ItemDoesntExistException();

            _userRepository.UnsubscribeNewsletter(user);
        }

        public void ChangePassword(string currentPassword, string newPassword, string jwtToken)
        {
            var JWTtoken = new JwtSecurityToken(jwtToken);
            string email = JWTtoken.Claims.FirstOrDefault(c => c.Type == "email").Value;

            var authenticationResult = IsPasswordCorrect(currentPassword, jwtToken);
            if (!authenticationResult)
                throw new UnauthorizedAccessException();

            _userRepository.ChangePassword(email, currentPassword, newPassword);
        }

        private bool IsPasswordCorrect(string password, string jwtToken)
        {
            var JWTtoken = new JwtSecurityToken(jwtToken);
            string email = JWTtoken.Claims.FirstOrDefault(c => c.Type == "email").Value;
            var result = _userRepository.IsPasswordCorrect(email, password);

            return result;
        }

        private string GetEmailFromToken(string jwtToken)
        {
            var JWTtoken = new JwtSecurityToken(jwtToken);
            string email = JWTtoken.Claims.FirstOrDefault(c => c.Type == "email").Value;

            return email;
        }
    }
}