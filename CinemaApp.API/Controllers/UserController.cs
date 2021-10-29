using CinemaApp.Domain.DTO.UserDTO;
using CinemaApp.Domain.Exceptions;
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
        public IActionResult AddUser(NewUserDTO user)
        {
            try
            {
                _userService.AddUser(user);
                return Ok("Succeed. Now you can log in.");
            }
            catch(ItemAlreadyExistsException)
            {
                return BadRequest("User with same email already exists");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }     
        }

        [HttpGet("getUserByToken")]
        public IActionResult GetUserByToken([FromHeader]string JwtToken)
        {
            try
            {
                var user = _userService.GetUserByToken(JwtToken);
                return Ok(user);
            }
            catch (ArgumentException)
            {
                return BadRequest("Token is incorrect");
            }
            catch (ItemDoesntExistException)
            {
                return BadRequest("User with given token doesn't exist");
            }
            catch(Exception)
            {
                return BadRequest("Token is empty");
            }
        }

        [HttpGet("all")]
        public IActionResult GetAllUsers([FromHeader] string JwtToken)
        {
            try
            {
                var users = _userService.GetAllUsers(JwtToken);
                return Ok(users);
            }
            catch(ArgumentException)
            {
                return Unauthorized();
            }
            catch(UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch(ListIsEmptyException)
            {
                return BadRequest("There aren't any users in database");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("deleteaccount")]
        public IActionResult DeleteAccount([FromHeader] string password, [FromHeader] string jwtToken)
        {
            try
            {
                _userService.DeleteAccount(password, jwtToken);
                return Ok();
                    
            }
            catch(ArgumentException)
            {
                return Unauthorized();
            }
            catch(UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getUserDiscount")]
        public IActionResult GetUserDiscount([FromHeader] string JwtToken)
        {
            try
            {
                var discount = _userService.GetUserDiscount(JwtToken);
                return Ok(discount);
            }
            catch(ArgumentException)
            {
                return Unauthorized();
            }
            catch(ItemDoesntExistException)
            {
                return BadRequest("User with given token doesn't exist");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("subscribeNewsletter")]
        public IActionResult SubscribeNewsletter([FromHeader] string JwtToken)
        {
            try
            {
                _userService.SubscribeNewsletter(JwtToken);
                return Ok("Newsletter subscribed");
            }
            catch(ArgumentException)
            {
                return Unauthorized();
            }
            catch(ItemDoesntExistException)
            {
                return BadRequest("User with given token doesn't exist");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("unsubscribeNewsletter")]
        public IActionResult UnsubscribeNewsletter([FromHeader] string JwtToken)
        {
            try
            {
                _userService.UnsubscribeNewsletter(JwtToken);
                return Ok("Newsletter unsubscribed");
            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }
            catch (ItemDoesntExistException)
            {
                return BadRequest("User with given token doesn't exist");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("changepassword")]
        public IActionResult IsPasswordCorrect([FromHeader]string currentPassword, [FromHeader]string newPassword, [FromHeader]string jwtToken)
        {
            try
            {
                _userService.ChangePassword(currentPassword, newPassword, jwtToken);
                return Ok();
            }
            catch(ArgumentException)
            {
                return Unauthorized();
            }
            catch(UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
    }
}
