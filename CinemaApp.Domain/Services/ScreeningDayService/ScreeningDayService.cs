using CinemaApp.DAL.Repositories.ScreeningDayRepository;
using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Domain.DTO.ScreeningDayDTOModels;
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

        public void AddScreeningDay(ScreeningDay screeningDay)
        {
            //check for errors
            _screeningDayRepository.AddScreeningDay(screeningDay);
        }

        public void DeleteScreeningDayById(int id)
        {
            //check for errors
            _screeningDayRepository.DeleteScreeningDayById(id);
        }

        public IEnumerable<ScreeningDay> GetAllScreeningDays()
        {
            //check for errors
            var screeningDays = _screeningDayRepository.GetAllScreeningDays();
            return screeningDays;
        }

        public IEnumerable<ScreeningDayDTO> GetAllScreeningDaysDTO()
        {
            var screeningDays = _screeningDayRepository.GetAllScreeningDays();
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
                        );;
                }
            }

            return screeningDaysDTO;
        }

        public ScreeningDay GetEntityById(int id)
        {
            //check for errors
            var screeningDay = _screeningDayRepository.GetEntityById(id);
            return screeningDay;
        }
    }
}
