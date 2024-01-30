using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Commands.Family;
using CleanArchitecture.Domain.Contracts.IRepositories;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infrastructure.Databases.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Databases.Repositories
{
    public class FamilyRepository<T> : BaseRepository<T>, IBaseRepository<T>, IFamilyRepository<T>
        where T : class
    {
        private readonly IMapper _mapper;
       

        public FamilyRepository(FamilyDBContext db) : base(db)
        {
           
        }

        public FamilyRepository(FamilyDBContext db, ILogger<FamilyRepository<T>> logger, IMapper mapper) : base(db, logger, mapper)
        {
            _mapper = mapper;
           
        }

        public async Task<Guid> Create(T t)
        {


            try
            {
                
                var entity = _mapper.Map<Family>(t);

                entity.AddedDate = DateTime.Now;
                _db.Families.Add(entity);
                await _db.SaveChangesAsync();


                return entity.Id;



            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Create Repository} All fucntion error", typeof(FamilyRepository<T>));
                throw;
            }
        }

        public async Task<int> Delete(Guid id)
        {


            try
            {

                await _db.Families
                          .Where(data => data.Id == id)
                          .ExecuteDeleteAsync();

                return 1;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Delete Repository} All fucntion error", typeof(FamilyRepository<T>));
                throw;
            }
        }



        public async Task<IEnumerable<T>> getByLastName(string lastname)
        {


            try
            {

                return await _db.Families
                            .Where(data => data.LastName == lastname)
                            .ProjectTo<T>(_mapper.ConfigurationProvider)
                            .ToListAsync();

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{GetFromId Repository} All fucntion error", typeof(FamilyRepository<T>));
                throw;
            }
        }


        public async Task<int> Update(Guid id, object model)
        {


            try
            {

                var data = await GetOne(id);

                var newdata = (UpdateFamilyCommand)model;
                var entitydata = _mapper.Map<Family>(data);

                entitydata.UpdateDate = DateTime.Now;

                if (newdata.status.HasValue) entitydata.status = (int)newdata.status;
                if (!newdata.FirstName.IsNullOrEmpty()) entitydata.FirstName = (string)newdata.FirstName;
                if (!newdata.LastName.IsNullOrEmpty()) entitydata.LastName = (string)newdata.LastName;
                if (newdata.Gender.HasValue) entitydata.Gender = (short)newdata.Gender;


                //var returndata = _mapper.Map<Family>(model);

                await _db.Families
                        .Where(model => model.Id == id)
                        .ExecuteUpdateAsync(setters => setters
                                .SetProperty(d => d.Id, entitydata.Id)
                                .SetProperty(d => d.UpdateDate, entitydata.UpdateDate)
                                .SetProperty(d => d.AddedDate, entitydata.AddedDate)
                                .SetProperty(d => d.FirstName, entitydata.FirstName)
                                .SetProperty(d => d.LastName, entitydata.LastName)
                                .SetProperty(d => d.status, entitydata.status)
                                .SetProperty(d => d.Gender, entitydata.Gender)
                                );


                return 1;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Update Repository} All fucntion error", typeof(FamilyRepository<T>));
                throw;
            }
        }

        public async Task<int> UpdateByLastname(Guid id, string lastname)
        {


            try
            {
                await _db.Families
                        .Where(data => data.Id == id)
                        .ExecuteUpdateAsync(setters => setters
                        .SetProperty(data => data.LastName, lastname)
                        );

                return 1;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Update Repository} All fucntion error", typeof(FamilyRepository<T>));
                throw;
            }
        }

    }
}
 