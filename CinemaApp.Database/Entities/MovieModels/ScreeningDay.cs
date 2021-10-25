using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Database.Entities.MovieModels
{
    public class ScreeningDay
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public List<Screening> Screenings { get; set; } = new List<Screening>();
    }
}
