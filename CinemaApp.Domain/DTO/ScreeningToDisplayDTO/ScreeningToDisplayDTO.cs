using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.DTO.ScreeningToDisplayDTO
{
    public class ScreeningToDisplayDTO
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public string PictureUrl { get; set; }
        public List<ScreeningHourDTO> ScreeningHours { get; set; }
    }
}
