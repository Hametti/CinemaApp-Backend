using CinemaApp.Database.Entities.Movie;
using CinemaApp.Domain.Services;
using CinemaApp.Domain.Services.DailyViewService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DailyViewController : Controller
    {
        private readonly IDailyViewService _dailyViewService;
        public DailyViewController(IDailyViewService dailyViewService)
        {
            _dailyViewService = dailyViewService;
        }

        [HttpGet("all")]
        public IActionResult GetAllDailyViews()
        {
            try
            {
                var dailyViews = _dailyViewService.GetDailyViews();
                return Ok(dailyViews);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("add")]
        public IActionResult AddDailyView([FromBody] DailyView dailyView)
        {
            //add modelstate validation
            try
            {
                _dailyViewService.AddDailyView(dailyView);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteDailyViewById(int id)
        {
            try
            {
                _dailyViewService.DeleteDailyViewById(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
