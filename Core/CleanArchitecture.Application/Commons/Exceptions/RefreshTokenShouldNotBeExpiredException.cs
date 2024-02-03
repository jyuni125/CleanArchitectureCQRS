using CleanArchitecture.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Commons.Exceptions
{
    public class RefreshTokenShouldNotBeExpiredException :BaseException
    {
        public RefreshTokenShouldNotBeExpiredException() : base("Your Refresh token session has expired, please log in again.") { }
    }
}
