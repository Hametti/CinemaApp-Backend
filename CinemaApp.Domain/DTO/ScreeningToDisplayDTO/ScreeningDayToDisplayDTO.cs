using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.DTO.ScreeningToDisplayDTO
{
    public class ScreeningDayToDisplayDTO
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public List<ScreeningToDisplayDTO> Screenings { get; set; }
    }
}
