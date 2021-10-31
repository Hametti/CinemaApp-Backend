using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Domain.DTO.ScreeningDayDTOModels;
using CinemaApp.Domain.Exceptions;
using CinemaApp.Domain.Services.ScreeningDayService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreeningDayController : Controller
    {
        private readonly IScreeningDayService _screeningDayService;
        public ScreeningDayController(IScreeningDayService screeningDayService)
        {
            _screeningDayService = screeningDayService;
        }

        [HttpGet("{id}")]
        public IActionResult GetScreeningDayById(int id)
        {
            try
            {
                var screeningDay = _screeningDayService.GetEntityById(id);
                return Ok(screeningDay);
            }
            catch(ItemDoesntExistException)
            {
                return BadRequest("ScreeningDay with given id doesn't exist");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("all")]
        public IActionResult GetAllScreeningDays()
        {
            try
            {
                var screeningDays = _screeningDayService.GetAllScreeningDays();
                return Ok(screeningDays);
            }
            catch(ListIsEmptyException)
            {
                return BadRequest("There aren't any screening days in database");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("all-dto")]
        public IActionResult GetAllScreeningDaysDTO()
        {
            try
            {
                var screeningDays = _screeningDayService.GetAllScreeningDays();
                return Ok(screeningDays);
            }
            catch(ListIsEmptyException)
            {
                return BadRequest("There aren't any screening days in database");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("add")]
        public IActionResult AddScreeningDay([FromBody]ScreeningDay screeningDay)
        {
            try
            {
                _screeningDayService.AddScreeningDay(screeningDay);
                return Ok();
            }
            catch(ItemAlreadyExistsException)
            {
                return BadRequest("ScreeningDay with same date already exists");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteScreeningDayById(int id)
        {
            try
            {
                _screeningDayService.DeleteScreeningDayById(id);
                return Ok();
            }
            catch(ItemDoesntExistException)
            {
                return BadRequest("ScreeningDay with given id doesn't exist");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
