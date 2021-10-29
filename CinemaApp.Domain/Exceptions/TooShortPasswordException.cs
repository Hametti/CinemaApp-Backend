using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Exceptions
{
    public class TooShortPasswordException : Exception
    {
        public TooShortPasswordException() { }
        public TooShortPasswordException(string message) : base(message) { }
    }
}
