using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Exceptions
{
    public class SeatIsOccupiedException : Exception
    {
        public SeatIsOccupiedException() { }
        public SeatIsOccupiedException(string message) : base(message) {}
    }
}
