﻿using CinemaApp.Database.Entities.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.DTO.ScreeningDayDTOModels
{
    public class ScreeningDTO
    {
        public int Id { get; set; }
        public MovieDTO Movie { get; set; }
        public List<ScreeningHour> ScreeningHours { get; set; } = new List<ScreeningHour>();
    }
}