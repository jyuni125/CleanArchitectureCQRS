using CleanArchitecture.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Commons.Exceptions
{
    public class EmailShouldNotBeInvalidException : BaseException
    {
        public EmailShouldNotBeInvalidException() :base("Email Address should be Valid"){ }
    }
}
