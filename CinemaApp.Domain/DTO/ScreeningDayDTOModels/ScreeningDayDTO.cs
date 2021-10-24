using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.DTO.ScreeningDayDTOModels
{
    public class ScreeningDayDTO
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public List<ScreeningDTO> Screenings { get; set; } = new List<ScreeningDTO>();
    }
}
