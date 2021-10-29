using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Database.Entities.MovieModels
{
    public class WeeklyDiscount
    {
        public int Id { get; set; }
        public Movie WeeklyDiscountMovie { get; set; }
        public int WeeklyDiscountValue { get; set; }
    }
}
