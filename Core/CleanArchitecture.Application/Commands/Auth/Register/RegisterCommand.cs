using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Commands.Auth.Register
{
    public class RegisterCommand : IRequest<string>
    {
        public string Fullname { get; set; }    
        public string Email { get; set; }
        public string Password { get; set; }    
        public string ConfirmPassword { get; set; }
    }
}
