using CinemaApp.Domain.DTO.Reservation;
using CinemaApp.Domain.Exceptions;
using CinemaApp.Domain.Services.ReservationService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("{id}")]
        public IActionResult GetReservationById([FromHeader]string jwtToken, int id)
        {
            try
            {
                var reservation = _reservationService.GetReservationById(jwtToken, id);
                return Ok(reservation);
            }
            catch(UnauthorizedAccessException)
            {
                return BadRequest("Unauthorized access");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("userreservations")]
        public IActionResult GetUserReservations([FromHeader]string jwtToken)
        {
            try
            {
                var reservations = _reservationService.GetUserReservations(jwtToken);
                return Ok(reservations);
            }
            catch(UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch(ListIsEmptyException)
            {
                return BadRequest("User with given token doesn't have any reservations");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("add")]
        public IActionResult AddReservation([FromHeader]string jwtToken, [FromBody]ReservationToAddDTO reservation)
        {
            try
            {
                _reservationService.AddReservation(jwtToken, reservation);
                return Ok();
            }
            catch(UnauthorizedAccessException)
            {
                return BadRequest("Unauthorized access");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteReservationById([FromHeader]string jwtToken, int id)
        {
            try
            {
                _reservationService.DeleteReservationById(jwtToken, id);
                return Ok();
            }
            catch(UnauthorizedAccessException)
            {
                return BadRequest("Unauthorized access");
            }
            catch(ItemDoesntExistException)
            {
                return BadRequest("Reservation doesn't exist or you don't have permission to see it");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
