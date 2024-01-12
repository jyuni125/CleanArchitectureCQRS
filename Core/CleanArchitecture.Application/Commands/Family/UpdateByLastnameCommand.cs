using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Commands.Family
{
    public class UpdateByLastnameFamilyCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public string Lastname { get; set; }
    }
}
