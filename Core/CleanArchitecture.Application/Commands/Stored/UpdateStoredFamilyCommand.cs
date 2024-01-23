using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Commands.Stored
{
    public class UpdateStoredFamilyCommand : IRequest<int>
    {

        public Guid Id { get; set; }
        public string? Firstname { get; set; } = string.Empty;
        public string? Lastname { get; set; } = string.Empty;
        public short? Gender { get; set; } = 0;
        public short? status { get; set; } = 0;
    }
}
