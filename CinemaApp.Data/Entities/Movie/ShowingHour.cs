using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Database.Entities.Movie
{
    public class ShowingHour
    {
        public int Id { get; set; }
        public string Hour { get; set; }
        public List<Movie> MovieList { get; set; } = new List<Movie>();
    }
}
