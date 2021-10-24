using CinemaApp.Database.Entities.Movie;
using CinemaApp.Domain.DTO.ScreeningDayDTOModels;
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

        [HttpGet("all")]
        public IActionResult GetAllScreeningDays()
        {
            var screeningDays = _screeningDayService.GetAllScreeningDays();
            return Ok(screeningDays);
        }

        [HttpGet("all-dto")]
        public IActionResult GetAllScreeningDaysDTO()
        {
            var screeningDays = _screeningDayService.GetAllScreeningDaysDTO();

            return Ok(screeningDays);
        }

        [HttpGet("{id}")]
        public IActionResult GetScreeningDayById(int id)
        {
            var screeningDay = _screeningDayService.GetEntityById(id);
            return Ok(screeningDay);
        }

        [HttpPost("add")]
        public IActionResult AddScreeningDay([FromBody]ScreeningDay screeningDay)
        {
            _screeningDayService.AddScreeningDay(screeningDay);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteScreeningDayById(int id)
        {
            _screeningDayService.DeleteScreeningDayById(id);
            return Ok();
        }
    }
}
