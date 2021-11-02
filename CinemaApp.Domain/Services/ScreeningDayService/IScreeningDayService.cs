using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Domain.DTO.ScreeningDayDTOModels;
using CinemaApp.Domain.DTO.ScreeningToDisplayDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Services.ScreeningDayService
{
    public interface IScreeningDayService
    {
        public ScreeningDayDTO GetEntityById(int id);
        public IEnumerable<ScreeningDayToDisplayDTO> GetAllScreeningDays();
        public void AddScreeningDay(ScreeningDay screeningDay);
        public void DeleteScreeningDayById(int id);
    }
}
