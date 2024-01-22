using CleanArchitecture.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Queries.Stored
{
    public class GetAllStoredFamilyQuery : IRequest<IEnumerable<StoredFamilyViewModel>>
    {
    }
}
