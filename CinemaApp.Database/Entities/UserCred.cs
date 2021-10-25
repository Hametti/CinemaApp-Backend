using CinemaApp.Database.Entities.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Database.Entities
{
    public class UserCred
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public User User { get; set; }
    }
}
