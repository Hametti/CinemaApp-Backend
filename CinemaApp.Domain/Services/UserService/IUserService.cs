using CinemaApp.Domain.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Services.UserService
{
    public interface IUserService
    {
        public void AddUser(UserDTO user);
    }
}
