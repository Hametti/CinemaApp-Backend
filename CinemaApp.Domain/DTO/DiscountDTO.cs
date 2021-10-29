using CinemaApp.Database.Entities.MovieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.DTO
{
    public class DiscountDTO
    {
        public Movie DiscountMovie { get; set; }
        public int DiscountValue { get; set; }
    }
}
