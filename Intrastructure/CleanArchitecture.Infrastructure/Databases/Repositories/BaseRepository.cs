using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Domain.Contracts.IRepositories;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infrastructure.Databases.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Databases.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class,IBaseModel
    {
        protected readonly FamilyDBContext _db;
        public readonly ILogger<BaseRepository<T>> _logger;
        private readonly IMapper _mapper;


        public BaseRepository(FamilyDBContext db)
        {
            _db = db;

        }

        public BaseRepository(FamilyDBContext db, ILogger<BaseRepository<T>> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<T>> GetAll()
        {


            try
            {
                _logger.LogInformation("GET ALL REPOSITORY WORKED!");


                return await _db.Families
                            .ProjectTo<T>(_mapper.ConfigurationProvider)
                            .ToListAsync();

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{GetALl Repository} All fucntion error", typeof(BaseRepository<T>));
                throw;
            }
        }

        public async Task<T> GetOne(Guid id)
        {


            try
            {
                _logger.LogInformation("GET BY ID REPOSITORY WORKED!");

                return await _db.Families
                            .Where(data => data.Id == id)
                            .ProjectTo<T>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync();

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{GetFromId Repository} All fucntion error", typeof(BaseRepository<T>));
                throw;
            }
        }
    }
}
