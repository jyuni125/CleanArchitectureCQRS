using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Commands.Auth.Revoke
{
    public class RevokeCommand : IRequest 
    {
        public string email { get; set; }
    }
}
