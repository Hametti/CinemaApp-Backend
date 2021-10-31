using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Database.Entities.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.DTO.ScreeningDayDTOModels
{
    public class ScreeningDTO
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public string Hour { get; set; }
        public List<SeatDTO> Seats { get; set; }
        public int ScreeningDayId { get; set; }
    }
}
