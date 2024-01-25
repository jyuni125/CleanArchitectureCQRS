using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Commands.Stored;
using CleanArchitecture.Application.DTOs.Stored;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Contracts.IRepositories;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enumerations;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infrastructure.Databases.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<Guid> Create(T t)
        {
            try
            {
                var data = _mapper.Map<StoredEntity>(t);


                data.Id = Guid.NewGuid(); 


                string sql = "exec createfamily @id,@firstname,@lastname,@gender,@status";

                SqlParameter[] parameter = { new SqlParameter("@id",data.Id.ToString()),
                    new SqlParameter("@firstname",(!string.IsNullOrEmpty(data.Firstname))?data.Firstname:""),
                    new SqlParameter("@lastname",(!string.IsNullOrEmpty(data.Lastname))?data.Lastname:""),
                    new SqlParameter("@gender",(!string.IsNullOrEmpty(data.Gender))?(short)(int)(Gender)Enum.Parse(typeof(Gender), data.Gender):2),
                    new SqlParameter("@status",data.status< 0?data.status:0)
                };


                 await _db.Database
                       .ExecuteSqlRawAsync(sql, parameter);


                return data.Id;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{GetALl Repository} All fucntion error", typeof(StoredFamilyRepository<T>));
                throw;
            };
        }

        public async Task<int> Delete(Guid id)
        {
            try
            {
                string sqlquery = "exec deleteById @id";



                SqlParameter parameter = new SqlParameter("@id", id);


                return await _db.Database
                         .ExecuteSqlRawAsync(sqlquery, parameter);

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
                string sqlquery = "select * from dbo.getallfamilies";

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

        public async Task<T> GetOne(Guid id)
        {
            try
            {
                string sqlquery = "select * from dbo.getallById(@id)";



                SqlParameter parameter = new SqlParameter("@id", id);


                return await _db.myStoredQuery
                  .FromSqlRaw(sqlquery,parameter)
                  .ProjectTo<T>(_mapper.ConfigurationProvider)
                  .FirstOrDefaultAsync();


            }
            catch (Exception e)
            {
                _logger.LogError(e, "{GetALl Repository} All fucntion error", typeof(StoredFamilyRepository<T>));
                throw;
            };
        }

        public async Task<int> Update(object model)
        {

            try
            {

                var data = (UpdateStoredFamilyCommand)model;
               
                var getModelData = await GetOne(data.Id);
                
                var entityData = _mapper.Map<StoredEntity>(getModelData);




                
                data.Firstname =(string.IsNullOrEmpty(data.Firstname)) ? entityData.Firstname : data.Firstname;
                data.Lastname = (string.IsNullOrEmpty(data.Lastname)) ? entityData.Lastname : data.Lastname;
                data.Gender = (!data.Gender.HasValue)? (short)(int)(Gender)Enum.Parse(typeof(Gender), entityData.Gender) : data.Gender;
                data.status = (!data.status.HasValue) ? entityData.status : data.status;
                
                
               


                string sql = "exec UpdateFamily @id,@firstname,@lastname,@gender,@status";

                SqlParameter[] parameter = { new SqlParameter("@id",data.Id.ToString()),
                                             new SqlParameter("@firstname",(!string.IsNullOrEmpty(data.Firstname))?data.Firstname:""),
                                             new SqlParameter("@lastname",(!string.IsNullOrEmpty(data.Lastname))?data.Lastname:""),
                                             new SqlParameter("@gender",data.Gender.HasValue? data.Gender:0),
                                             new SqlParameter("@status",data.status.HasValue?data.status:0)
                                            };

                return await _db.Database
                         .ExecuteSqlRawAsync(sql,parameter);

               
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
