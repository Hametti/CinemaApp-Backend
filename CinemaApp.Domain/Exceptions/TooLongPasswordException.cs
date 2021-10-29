using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Exceptions
{
    public class TooLongPasswordException : Exception
    {
        public TooLongPasswordException() { }
        public TooLongPasswordException(string message) : base(message) { }
    }
}
