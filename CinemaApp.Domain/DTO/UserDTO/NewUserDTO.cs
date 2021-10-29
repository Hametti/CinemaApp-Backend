using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.DTO.UserDTO
{
    public class NewUserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityQuestionAnswer { get; set; }
    }
}
