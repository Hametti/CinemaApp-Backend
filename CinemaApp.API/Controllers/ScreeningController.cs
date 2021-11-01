using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Domain.DTO;
using CinemaApp.Domain.Exceptions;
using CinemaApp.Domain.Services.ScreeningService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreeningController : Controller
    {
        private readonly IScreeningService _screeningService;
        public ScreeningController(IScreeningService screeningService)
        {
            _screeningService = screeningService;
        }

        [HttpGet("{id}")]
        public IActionResult GetScreeningById(int id)
        {
            try
            {
                var screening = _screeningService.GetEntityById(id);
                return Ok(screening);
            }
            catch (ItemDoesntExistException)
            {
                return BadRequest("Screening with given id doesn't exist");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("all")]
        public IActionResult GetAllScreenings()
        {
            try
            {
                var screenings = _screeningService.GetAllScreenings();
                return Ok(screenings);
            }
            catch(ListIsEmptyException)
            {
                return BadRequest("There aren't any screenings in database");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/seats")]
        public IActionResult GetAllScreeningSeats(int id)
        {
            try
            {
                var seats = _screeningService.GetAllScreeningSeats(id);
                return Ok(seats);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("add")]
        public IActionResult AddScreening([FromBody]ScreeningToAddDTO screeningDTO)
        {
            try
            {
                _screeningService.AddScreening(screeningDTO);
                return Ok();
            }
            catch (ItemAlreadyExistsException)
            {
                return BadRequest("Screening with same date and hour already exists");
            }
            catch (ItemDoesntExistException)
            {
                return BadRequest("ScreeningDay with given id doesn't exist");
            }
            catch (ArgumentException)
            {
                return BadRequest("Given hour is incorrect. Try something like this \"12:00\"");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("delete/{id}")]
        public IActionResult DeleteScreeningById(int id)
        {
            try
            {
                _screeningService.DeleteScreeningById(id);
                return Ok();
            }
            catch(ItemDoesntExistException)
            {
                return BadRequest("Screening with given id doesn't exist");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
