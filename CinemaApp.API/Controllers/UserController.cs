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
        public IActionResult AddUser(UserCredDTO user)
        {
           // _userService.AddUser(user);
            return Ok("Succeed. Now you can log in.");
        }

        [HttpGet("getUserByToken")]
        public IActionResult GetUserByToken([FromHeader]string JwtToken)
        {
            var user = _userService.GetUserByToken(JwtToken);
            return Ok(user);
        }

        [HttpGet("getUserDiscount")]
        public IActionResult GetUserDiscount([FromHeader] string JwtToken)
        {
            var discount = _userService.GetUserDiscount(JwtToken);
            return Ok(discount);
        }

        [HttpGet("subscribeNewsletter")]
        public IActionResult SubscribeNewsletter([FromHeader] string JwtToken)
        {
            _userService.SubscribeNewsletter(JwtToken);
            return Ok("Newsletter subscribed");
        }

        [HttpGet("unsubscribeNewsletter")]
        public IActionResult UnsubscribeNewsletter([FromHeader] string JwtToken)
        {
            _userService.UnsubscribeNewsletter(JwtToken);
            return Ok("Newsletter unsubscribed");
        }
    }
}
