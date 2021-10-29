using CinemaApp.Auth.Classes;
using CinemaApp.Auth.Interfaces;
using CinemaApp.DAL.Repositories.Authentication;
using CinemaApp.DAL.Repositories.UserRepository;
using CinemaApp.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        private readonly IAuthenticationRepository authenticationRepository;
        private readonly IUserRepository userRepository;
        public MainController(IJwtAuthenticationManager jwtAuthenticationManager, IAuthenticationRepository authenticationRepository, IUserRepository userRepository)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this.authenticationRepository = authenticationRepository;
            this.userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Authenticated access");
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            var userCredCorrect = authenticationRepository.AreUserCredsCorrect(userCred.Email, userCred.Password);

            if (userCredCorrect)
            {
                string role;
                if (userRepository.GetUserByEmail(userCred.Email).IsAdmin)
                    role = "admin";
                else
                    role = "user";

                var token = jwtAuthenticationManager.Authenticate(userCred.Email, userCred.Password, role);
                return Ok(token);
            }

            else
                return Unauthorized(null);
            
        }

    }
}
