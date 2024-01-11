using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Contracts.IRepositories
{
    public interface IBaseRepository<T>
    {
        Task<T> GetOne(Guid id);

        Task<IEnumerable<T>> GetAll();
    }
}
