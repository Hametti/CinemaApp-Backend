using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Database.Entities.MovieModels;

namespace CinemaApp.Database.Entities.UserModels
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public bool Subscription { get; set; } = false;
        public Movie UniqueDiscount { get; set; } = null;
        public int UniqueDiscountValue { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityQuestionAnswer { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
