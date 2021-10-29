using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Exceptions
{
    public class ListIsNotEmptyException : Exception
    {
        public ListIsNotEmptyException() { }
        public ListIsNotEmptyException(string message) : base(message) { }
    }
}
