using CinemaApp.Database.Entities.MovieModels;
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

        [HttpGet("all")]
        public IActionResult GetAllScreenings()
        {
            var screenings = _screeningService.GetAllScreenings();
            return Ok(screenings);
        }

        [HttpGet("{id}")]
        public IActionResult GetScreeningById(int id)
        {
            var screening = _screeningService.GetEntityById(id);
            return Ok(screening);
        }

        [HttpPost("add")]
        public IActionResult AddScreening([FromBody]Screening screening)
        {
            _screeningService.AddScreening(screening);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteScreeningById(int id)
        {
            _screeningService.DeleteScreeningById(id);
            return Ok();
        }
    }
}
