using CinemaApp.Database.Entities.MovieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.DTO.UserDTO
{
    public class WeeklyDiscountMovieDTO
    {
        public Movie DiscountMovie { get; set; }
        public int DiscountValue { get; set; }
    }
}
