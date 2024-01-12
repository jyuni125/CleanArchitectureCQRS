using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Commands.Family
{
    public class DeleteFamilyCommand : IRequest<int>
    {
        [Required]
        public Guid Id { get; set; }


    }
}
