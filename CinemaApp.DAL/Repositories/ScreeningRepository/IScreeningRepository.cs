﻿using CinemaApp.DAL.Repositories.BaseRepository;
using CinemaApp.Database.Entities.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories.ScreeningRepository
{
    public interface IScreeningRepository : IBaseRepository<Screening>
    {
        public IEnumerable<Screening> GetAllScreenings();
        public void AddScreening(Screening screening);
        public void DeleteScreeningById(int id);
    }
}