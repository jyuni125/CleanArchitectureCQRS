using CleanArchitecture.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Commons.Exceptions
{
    public class EmailConfirmationException : BaseException
    {
        public EmailConfirmationException() : base("Pls Check your Email,Email should be verified ") { }
    }
}
