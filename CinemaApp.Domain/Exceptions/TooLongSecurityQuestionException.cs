﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Exceptions
{
    public class TooLongSecurityQuestionException : Exception
    {
        public TooLongSecurityQuestionException() { }
        public TooLongSecurityQuestionException(string message) : base(message) { }
    }
}
