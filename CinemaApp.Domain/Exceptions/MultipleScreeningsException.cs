using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Exceptions
{
    public class MultipleScreeningsException : Exception
    {
        public MultipleScreeningsException() { }

        public MultipleScreeningsException(string message) : base(message) { }
    }
}
