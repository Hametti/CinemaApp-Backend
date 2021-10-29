using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Exceptions
{
    public class TooShortNameException : Exception
    {
        public TooShortNameException() { }
        public TooShortNameException(string message) : base(message) { }
    }
}
