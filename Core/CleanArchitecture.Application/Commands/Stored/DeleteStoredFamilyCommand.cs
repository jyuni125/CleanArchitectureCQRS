using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Commands.Stored
{
    public class DeleteStoredFamilyCommand : IRequest<int>
    {
        public Guid id { get;set; }
    }
}
