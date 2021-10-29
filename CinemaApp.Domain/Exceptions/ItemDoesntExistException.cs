using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Exceptions
{
    public class ItemDoesntExistException : Exception
    {
        public ItemDoesntExistException() { }
        public ItemDoesntExistException(string message) : base(message) { }
    }
}
