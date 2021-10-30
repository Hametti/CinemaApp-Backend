using CinemaApp.DAL.Repositories.ScreeningDayRepository;
using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Domain.DTO.ScreeningDayDTOModels;
using CinemaApp.Domain.Exceptions;
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

        public ScreeningDay GetEntityById(int id)
        {
            var screeningDay = _screeningDayRepository.GetEntityById(id);
            if (screeningDay == null)
                throw new ItemDoesntExistException();

            return screeningDay;
        }
        public IEnumerable<ScreeningDay> GetAllScreeningDays()
        {
            var screeningDays = _screeningDayRepository.GetAllScreeningDays().ToList();
            if (screeningDays == null || screeningDays.Count == 0)
                throw new ListIsEmptyException();

            return screeningDays;
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

        public IEnumerable<ScreeningDayDTO> GetAllScreeningDayDTOs()
        {
            var screeningDays = _screeningDayRepository.GetAllScreeningDays().ToList();
            if (screeningDays == null || screeningDays.Count == 0)
                throw new ListIsEmptyException();

            var screeningDaysDTO = new List<ScreeningDayDTO>();
            
            foreach(ScreeningDay screeningDay in screeningDays)
            {
                screeningDaysDTO.Add(
                    new ScreeningDayDTO
                    { 
                        Id = screeningDay.Id,
                        Date = screeningDay.Date
                    });

                var screeningDto = screeningDaysDTO.FirstOrDefault(s => s.Id == screeningDay.Id);

                foreach (Screening screening in screeningDay.Screenings)
                {
                    screeningDto.Screenings.Add
                    (
                        new ScreeningDTO
                        {
                            Id = screening.Id,
                            Hour = screening.Hour,
                            Movie = new MovieDTO
                            {
                                Id = screening.Movie.Id,
                                PictureUrl = screening.Movie.PictureUrl,
                                Title = screening.Movie.Title
                            }
                        }
                    );
                }
            }

            return screeningDaysDTO;
        }
    }
}
