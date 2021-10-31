using CinemaApp.DAL.Repositories.ScreeningDayRepository;
using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Domain.DTO.ScreeningDayDTOModels;
using CinemaApp.Domain.Exceptions;
using CinemaApp.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Services.ScreeningDayService
{
    public class ScreeningDayService : IScreeningDayService
    {
        private readonly IScreeningDayRepository _screeningDayRepository;
        public ScreeningDayService(IScreeningDayRepository screeningDayRepository)
        {
            _screeningDayRepository = screeningDayRepository;
        }

        public ScreeningDayDTO GetEntityById(int id)
        {
            var screeningDay = _screeningDayRepository.GetEntityById(id);
            if (screeningDay == null)
                throw new ItemDoesntExistException();

            var screeningDayDTO = DTOHelper.ScreeningDayToDTO(screeningDay);
            return screeningDayDTO;
        }
        public IEnumerable<ScreeningDayDTO> GetAllScreeningDays()
        {
            var screeningDays = _screeningDayRepository.GetAllScreeningDays().ToList();
            if (screeningDays == null || screeningDays.Count == 0)
                throw new ListIsEmptyException();

            var screeningDaysDTO = DTOHelper.ScreeningDaysToDTOs(screeningDays);
            return screeningDaysDTO;
        }

        public void AddScreeningDay(ScreeningDay screeningDay)
        {
            var screeningDayFromDb = _screeningDayRepository.GetAllScreeningDays().FirstOrDefault(s => s.Date == screeningDay.Date);
            if (screeningDayFromDb != null)
                throw new ItemAlreadyExistsException();

            _screeningDayRepository.AddScreeningDay(screeningDay);
        }

        public void DeleteScreeningDayById(int id)
        {
            var screeningDay = _screeningDayRepository.GetEntityById(id);
            if (screeningDay == null)
                throw new ItemDoesntExistException();

            _screeningDayRepository.DeleteScreeningDayById(id);
        }
    }
}
