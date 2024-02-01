using CleanArchitecture.Domain.Models.AuthResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Commands.Auth.Login
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
