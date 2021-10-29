using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Domain.DTO.UserDTO;
using CinemaApp.Domain.Exceptions;
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
            try
            {
                var movies = _movieService.GetAllMovies();
                return Ok(movies);
            }
            catch(ListIsEmptyException)
            {
                return BadRequest("There aren't any movies in the database");
            }
            catch(Exception e)
            {
                return BadRequest($"Something went wrong: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetMovieById(int id)
        {
            try
            {
                var movie = _movieService.GetEntityById(id);
                return Ok(movie);
            }
            catch(ItemDoesntExistException)
            {
                return BadRequest("Movie with given id doesn't exist");
            }
            catch(Exception e)
            {
                return BadRequest($"Something went wrong: {e.Message}");
            }
        }

        [HttpGet("five")]
        public IActionResult GetFiveMovies()
        {
            try
            {
                var movies = _movieService.GetFivemovies();
                return Ok(movies);
            }
            catch (ListIsEmptyException)
            {
                return BadRequest("There aren't any movies in the database");
            }
            catch (NotEnoughMoviesException)
            {
                return BadRequest("There aren't enough movies in the database");
            }
            catch(Exception e)
            {
                return BadRequest($"Something went wrong: {e.Message}");
            }
        }

        [HttpGet("weeklydiscountmovie")]
        public IActionResult GetWeeklyDiscountMovie()
        {
            try
            {
                var weeklyDiscount = _movieService.GetWeeklyDiscountMovie();
                return Ok(weeklyDiscount);
            }
            catch(ListIsEmptyException)
            {
                return BadRequest("There aren't any movies in the database");
            }
            catch(Exception e)
            {
                return BadRequest($"Something went wrong: {e.Message}");
            }
        }

        //check for exceptions after implementing admin panel
        [HttpPost("add")]
        public IActionResult AddMovie([FromBody] Movie movie)
        {
            try
            {
                _movieService.AddMovie(movie);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Something went wrong: {e.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteMovieById(int id)
        {
            try
            {
                _movieService.DeleteMovieById(id);
                return Ok();
            }
            catch(ItemDoesntExistException)
            {
                return BadRequest("Movie with given id doesn't exist");
            }
            catch (Exception e)
            {
                return BadRequest($"Something went wrong: {e.Message}");
            }
        }
    }
}
