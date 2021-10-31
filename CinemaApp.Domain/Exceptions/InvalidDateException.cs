using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Exceptions
{
    public class InvalidDateException : Exception
    {
        public InvalidDateException() { }
        public InvalidDateException(string message) : base(message) { }
    }
}
