using CinemaApp.Auth.Classes;
using CinemaApp.Auth.Interfaces;
using CinemaApp.DAL.Repositories.Authentication;
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
        public MainController(IJwtAuthenticationManager jwtAuthenticationManager, IAuthenticationRepository authenticationRepository)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this.authenticationRepository = authenticationRepository;
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
            var userCredCorrect = authenticationRepository.CheckUserCreds(userCred.Username, userCred.Password);

            if (userCredCorrect)
            {
                var token = jwtAuthenticationManager.Authenticate(userCred.Username, userCred.Password);
                return Ok(token);
            }

            else
                return Unauthorized(null);
            
        }

    }
}
