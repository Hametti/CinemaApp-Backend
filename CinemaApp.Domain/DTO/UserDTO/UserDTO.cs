﻿using CinemaApp.Database.Entities.MovieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.DTO.UserDTO
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public Movie DiscountMovie{ get; set; }
        public bool Subscription { get; set; }
    }
}