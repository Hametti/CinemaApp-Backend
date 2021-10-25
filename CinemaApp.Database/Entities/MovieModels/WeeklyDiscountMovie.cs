using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Database.Entities.MovieModels
{
    public class WeeklyDiscountMovie
    {
        public int Id { get; set; }
        public Movie WeeklyDiscount { get; set; }
        public int WeeklyDiscountValue { get; set; }
    }
}
