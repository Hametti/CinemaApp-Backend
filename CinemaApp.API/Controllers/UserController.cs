using CinemaApp.Domain.DTO.UserDTO;
using CinemaApp.Domain.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
        public IActionResult AddUser(UserDataDTO user)
        {
            _userService.AddUser(user);
            return Ok("Succeed. Now you can log in.");
        }

        [HttpGet("getUserByToken")]
        public IActionResult GetUserByToken([FromHeader]string JwtToken)
        {
            try
            {
                var user = _userService.GetUserByToken(JwtToken);
                return Ok(user);
            }
            catch(Exception)
            {
                return BadRequest("Token is empty");
            }
            
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

        [HttpPost("changepassword")]
        public IActionResult IsPasswordCorrect([FromHeader]string currentPassword, [FromHeader]string newPassword, [FromHeader]string jwtToken)
        {
            var result = _userService.ChangePassword(currentPassword, newPassword, jwtToken);
            return Ok(result);
        }

        [HttpPost("deleteaccount")]
        public IActionResult DeleteAccount([FromHeader]string password, [FromHeader]string jwtToken)
        {
            try
            {
                var result = _userService.DeleteAccount(password, jwtToken);
                if (result)
                    return Ok();
                else
                    return Unauthorized("Your password isn't correct");
            }
            catch(Exception)
            {
                return BadRequest("Something went wrong");
            }
            
        }
    }
}
