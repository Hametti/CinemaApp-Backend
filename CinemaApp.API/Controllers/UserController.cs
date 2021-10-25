using CinemaApp.Domain.DTO.UserDTO;
using CinemaApp.Domain.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("add")]
        public IActionResult AddUser(UserDTO user)
        {
            _userService.AddUser(user);
            return Ok("Succeed. Now you can log in.");
        }
    }
}
