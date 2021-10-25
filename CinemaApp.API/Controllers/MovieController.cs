using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Domain.DTO.UserDTO;
using CinemaApp.Domain.Services.MovieService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("all")]
        public IActionResult GetAllMovies()
        {
            var movies = _movieService.GetAllMovies();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public IActionResult GetMovieById(int id)
        {
            var movie = _movieService.GetEntityById(id);
            return Ok(movie);
        }

        [HttpGet("five")]
        public IActionResult GetFiveMovies()
        {
            var movies = _movieService.GetFivemovies();
            return Ok(movies);
        }

        [HttpGet("weeklydiscountmovie")]
        public IActionResult GetWeeklyDiscountMovie()
        {
            var weeklyDiscount = _movieService.GetWeeklyDiscountMovie();
            return Ok(weeklyDiscount);
        }

        [HttpPost("add")]
        public IActionResult AddMovie([FromBody] Movie movie)
        {
            _movieService.AddMovie(movie);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteMovieById(int id)
        {
            _movieService.DeleteMovieById(id);
            return Ok();
        }
    }
}
