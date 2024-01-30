using CleanArchitecture.Application.Commons.Results;
using CleanArchitecture.Domain.Contracts.IRepositories;
using CleanArchitecture.Domain.Contracts.IServices.IServices;
using CleanArchitecture.Domain.Contracts.IServices.IServicesFacades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Services
{
    public class FamilyServices<T> : IFamilyServices<T>
    {
        private readonly IFamilyRepository<T> _repo;
        private readonly IRestCall _restclient;
        public FamilyServices(IFamilyRepository<T> repo,IRestCall restclient)
        {
            _repo = repo;
            _restclient = restclient;
        }

        public async Task<Guid> Create(T t)
        {
           return await _repo.Create(t);
        }

        public async Task<int> Delete(Guid id)
        {
            return await _repo.Delete(id);
        }

        public QueryOrCommandResult<T1> DeleteRestcall<T1>(string uri, string endpoints, Guid id)
        {
            return  _restclient.DeleteRestcall<T1>(uri, endpoints, id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<IEnumerable<T>> getByLastName(string Lastname)
        {
            return await _repo.getByLastName(Lastname);
        }

        public async Task<T> GetOne(Guid id)
        {
            return await _repo.GetOne(id);
        }

        public QueryOrCommandResult<T1> GetRestcall<T1>(string uri, string endpoints)
        {
            return _restclient.GetRestcall<T1>(uri, endpoints);
        }

        public QueryOrCommandResult<T1> PostRestcall<T1>(string uri, string endpoints, object dto)
        {
            return _restclient.PostRestcall<T1>(uri, endpoints, dto);
        }

        public QueryOrCommandResult<T1> PutRestcall<T1>(string uri, string endpoints, object dto)
        {
            return _restclient.PostRestcall<T1>(uri, endpoints, dto);
        }

        public async Task<int> Update(Guid id, object model)
        {
            return await _repo.Update(id, model);
        }

        public async Task<int> UpdateByLastname(Guid id, string lastname)
        {
            return await _repo.UpdateByLastname(id, lastname);
        }
    }
}
