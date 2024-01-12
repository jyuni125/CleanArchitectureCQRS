using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Commands.Family
{
    public class UpdateFamilyCommand : IRequest<int>
    {
        public Guid Id { get; set; }

        public int? status { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public short? Gender { get; set; }
    }
}
