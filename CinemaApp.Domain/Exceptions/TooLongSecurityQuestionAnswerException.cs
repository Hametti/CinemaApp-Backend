using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Exceptions
{
    public class TooLongSecurityQuestionAnswerException : Exception
    {
        public TooLongSecurityQuestionAnswerException() { }
        public TooLongSecurityQuestionAnswerException(string message) : base(message) { }
    }
}
