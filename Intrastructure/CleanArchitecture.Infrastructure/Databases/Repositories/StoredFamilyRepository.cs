using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Domain.Contracts.IRepositories;
using CleanArchitecture.Infrastructure.Databases.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Databases.Repositories
{
    public class StoredFamilyRepository<T> : IStoredFamilyRepository<T>
    {
        protected readonly FamilyDBContext _db;
        public readonly ILogger<StoredFamilyRepository<T>> _logger;
        private readonly IMapper _mapper;

        public StoredFamilyRepository(FamilyDBContext db,ILogger<StoredFamilyRepository<T>> logger,IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
            _logger = logger;
        }

        public Task<Guid> Create(T t)
        {
            try
            {
                throw new NotImplementedException();

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{GetALl Repository} All fucntion error", typeof(StoredFamilyRepository<T>));
                throw;
            };
        }

        public Task<int> Delete(Guid id)
        {
            try
            {
                throw new NotImplementedException();

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{GetALl Repository} All fucntion error", typeof(StoredFamilyRepository<T>));
                throw;
            };
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                string sqlquery = "select * from getallfamilies";

                return await _db.myStoredQuery
                    .FromSqlRaw(sqlquery)
                    .ProjectTo<T>(_mapper.ConfigurationProvider)
                    .ToListAsync();

               

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{GetALl Repository} All fucntion error", typeof(StoredFamilyRepository<T>));
                throw;
            };
        }

        public Task<T> GetOne(Guid id)
        {
            try
            {
                throw new NotImplementedException();

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{GetALl Repository} All fucntion error", typeof(StoredFamilyRepository<T>));
                throw;
            };
        }

        public Task<int> Update(Guid id, object model)
        {

            try
            {
                throw new NotImplementedException();

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{GetALl Repository} All fucntion error", typeof(StoredFamilyRepository<T>));
                throw;
            };
        }

        public async Task<IEnumerable<T>> GetByGender(string gender)
        {
            try
            {
                /*
                string sqlquery = $"select * from dbo.getallbyGender({gender})";


                return await _db.myStoredQuery
                   .FromSqlRaw(sqlquery)    
                   .ProjectTo<T>(_mapper.ConfigurationProvider)
                   .ToListAsync();

                */

                string sqlquery = "select * from dbo.getallbyGender(@gender)";

                SqlParameter parameter = new SqlParameter("@gender", gender);

                
                return await _db.myStoredQuery
                  .FromSqlRaw(sqlquery,parameter)
                  .ProjectTo<T>(_mapper.ConfigurationProvider)
                  .ToListAsync();

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{GetALl Repository} All fucntion error", typeof(StoredFamilyRepository<T>));
                throw;
            };
        }
    }
}
