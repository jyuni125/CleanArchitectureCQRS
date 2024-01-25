using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Commands.Stored
{
    public class CreateStoredFamilyCommand : IRequest<Guid>
    {
       
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public short Gender { get; set; }
        public int status { get; set; }
    }
}
