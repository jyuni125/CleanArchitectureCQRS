using CleanArchitecture.Domain.Models.AuthResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Commands.Auth.RefreshToken
{
    public class RefreshTokenCommand : IRequest<RefreshTokenResponse>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
