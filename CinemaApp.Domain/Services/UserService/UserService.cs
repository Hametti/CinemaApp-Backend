using CinemaApp.DAL.Repositories.UserRepository;
using CinemaApp.Database.Entities;
using CinemaApp.Database.Entities.UserModels;
using CinemaApp.Domain.DTO.UserDTO;
using CinemaApp.Domain.Services.MovieService;
using System;
using System.Collections.Generic;
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

        public void AddUser(UserDTO user)
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
    }
}
