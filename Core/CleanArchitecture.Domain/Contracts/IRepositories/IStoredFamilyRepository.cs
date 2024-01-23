using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Contracts.IRepositories
{
    public interface IStoredFamilyRepository<T> : IBaseRepository<T>
    {
        public Task<Guid> Create(T t);

        public Task<int> Update(object model);

        public Task<int> Delete(Guid id);

        public Task<IEnumerable<T>> GetByGender(string gender);
    }
}
