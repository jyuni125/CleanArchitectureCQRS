using CleanArchitecture.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Queries.Family
{
    public class GetAllFamilyByLastnameQuery : IRequest<IEnumerable<FamilyViewModel>>
    {
        public string Lastname { get; set; } = string.Empty;
    }
}
