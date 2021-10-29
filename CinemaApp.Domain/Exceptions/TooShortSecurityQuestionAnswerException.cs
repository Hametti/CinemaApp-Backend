using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Exceptions
{
    public class TooShortSecurityQuestionAnswerException : Exception
    {
        public TooShortSecurityQuestionAnswerException() { }
        public TooShortSecurityQuestionAnswerException(string message) : base(message) { }
    }
}
