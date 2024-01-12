using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CleanArchitecture.Application.Queries.Family
{
    public class GetAllFamilyByIdQuery : IRequest<FamilyViewModel>
    {
        public Guid Id { get; set; }

    }
}
