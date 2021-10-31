using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Domain.DTO;
using CinemaApp.Domain.DTO.ScreeningDayDTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Services.ScreeningService
{
    public interface IScreeningService
    {
        public ScreeningDTO GetEntityById(int id);
        public void AddScreening(ScreeningToAddDTO screening, int screeningDayId);
        public IEnumerable<ScreeningDTO> GetAllScreenings();
        public void DeleteScreeningById(int id);
    }
}
