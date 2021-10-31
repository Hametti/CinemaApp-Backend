using CinemaApp.Database.Entities.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.DTO
{
    public class ScreeningToAddDTO
    {
        public int movieId { get; set; }
        public string Hour { get; set; }
        public List<Seat> Seats { get; set; } = new List<Seat>();
    }
}
