using CinemaApp.DAL.Repositories.MovieRepository;
using CinemaApp.DAL.Repositories.ScreeningDayRepository;
using CinemaApp.DAL.Repositories.ScreeningRepository;
using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Database.Entities.UserModels;
using CinemaApp.Domain.DTO;
using CinemaApp.Domain.DTO.ScreeningDayDTOModels;
using CinemaApp.Domain.DTO.SeatsToDisplay;
using CinemaApp.Domain.Exceptions;
using CinemaApp.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CinemaApp.Domain.Services.ScreeningService
{
    public class ScreeningService : IScreeningService
    {
        private readonly IScreeningRepository _screeningRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IScreeningDayRepository _screeningDayRepository;
        public ScreeningService(IScreeningRepository screeningRepository, IMovieRepository movieRepository, IScreeningDayRepository screeningDayRepository)
        {
            _screeningRepository = screeningRepository;
            _movieRepository = movieRepository;
            _screeningDayRepository = screeningDayRepository;
        }

        public void AddScreening(ScreeningToAddDTO screeningDTO)
        {
            var screeningDay = _screeningDayRepository.GetEntityById(screeningDTO.ScreeningDayId);
            if (screeningDay == null)
                throw new ItemDoesntExistException();

            var screeningFromDb = _screeningRepository
                .GetAllScreenings()
                .FirstOrDefault(s => s.Hour == screeningDTO.Hour && s.ScreeningDay.Date == screeningDay.Date);
            
            if (screeningFromDb != null)
                throw new ItemAlreadyExistsException();
           
            if (screeningDTO.Hour.Length < 2 || screeningDTO.Hour.Length > 5)
                throw new ArgumentException();

            var screeningMovie = _movieRepository.GetEntityById(screeningDTO.MovieId);
            if (screeningMovie == null)
                throw new ItemDoesntExistException();

            var screening = new Screening
            {
                Movie = _movieRepository.GetEntityById(screeningDTO.MovieId),
                Hour = screeningDTO.Hour,
                Seats = GenerateSeats()
            };

            _screeningRepository.AddScreening(screening, screeningDTO.ScreeningDayId);
        }
        public IEnumerable<ScreeningDTO> GetAllScreenings()
        {
            var screenings = _screeningRepository.GetAllScreenings().ToList();
            if (screenings == null && screenings.Count == 0)
                throw new ListIsEmptyException();

            var screeningDTOs = DTOHelper.ScreeningsToDTOs(screenings);
            return screeningDTOs;
        }
        public ScreeningDTO GetEntityById(int id)
        {
            var screening = _screeningRepository.GetEntityById(id);
            if (screening == null)
                throw new ItemDoesntExistException();

            var screeningDTO = DTOHelper.ScreeningToDTO(screening);
            return screeningDTO;
        }

        public void DeleteScreeningById(int id)
        {
            var screening = _screeningRepository.GetEntityById(id);
            if (screening == null)
                throw new ItemDoesntExistException();

            _screeningRepository.DeleteScreeningById(id);
        }

        private List<Seat> GenerateSeats()
        {
            var seats = new List<Seat>();

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 10; j++)
                {
                    seats.Add(
                        new Seat
                        {
                            Row = i + 1,
                            SeatNumber = j + 1
                        });
                }

            for (int i = 8; i < 10; i++)
                for (int j = 0; j < 14; j++)
                    seats.Add(
                        new Seat
                        {
                            Row = i + 1,
                            SeatNumber = j + 1
                        });

            return seats;
        }

        public IEnumerable<SeatRowDTO> GetAllScreeningSeats(int id)
        {
            var seats = _screeningRepository.GetAllScreeningSeats(id);
            var seatRowDTOs = DTOHelper.SeatsToSeatRowDTOs(seats);
            return seatRowDTOs;
        }

        public ScreeningInfoDTO GetScreeningInfoById(int id)
        {
            var screening = _screeningRepository.GetEntityById(id);
            var screeningInfoDTO = DTOHelper.ScreeningToScreeningInfoDTO(screening);
            
            return screeningInfoDTO;
        }
    }
}
