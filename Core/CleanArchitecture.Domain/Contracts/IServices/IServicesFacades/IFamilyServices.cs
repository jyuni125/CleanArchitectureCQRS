using CleanArchitecture.Domain.Contracts.IRepositories;
using CleanArchitecture.Domain.Contracts.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Contracts.IServices.IServicesFacades
{
    public interface IFamilyServices<T> : IFamilyRepository<T>,IRestCall
    {
    }
}
