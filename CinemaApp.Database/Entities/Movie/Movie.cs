using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Database.Entities.Movie
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DailyView> DailyViewList { get; set; }
        public List<ShowingHour> ShowingHourList { get; set; }
    }
}
