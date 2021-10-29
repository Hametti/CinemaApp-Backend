﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Exceptions
{
    public class TooShortSecurityQuestionException : Exception
    {
        public TooShortSecurityQuestionException() { }
        public TooShortSecurityQuestionException(string message) : base(message) { }
    }
}
