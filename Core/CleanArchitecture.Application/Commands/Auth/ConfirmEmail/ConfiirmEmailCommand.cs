using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Commands.Auth.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<string>
    {
        public string userId { get; set; }
        public string token { get; set; }
    }
}
