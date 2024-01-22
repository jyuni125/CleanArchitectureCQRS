using AutoMapper;
using CleanArchitecture.Application.Queries.Stored;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Contracts.IRepositories;
using CleanArchitecture.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.QueryHandlers
{
    public class StoredFamilyQueryHandlers : IRequestHandler<GetAllStoredFamily, IEnumerable<StoredFamilyViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IStoredFamilyRepository<StoredFamilyModel> _storedfamilyRepository;
        public StoredFamilyQueryHandlers(IMapper mapper, IStoredFamilyRepository<StoredFamilyModel> storedfamilyRepository)
        {
            _mapper = mapper;
            _storedfamilyRepository = storedfamilyRepository;
        }


        public async Task<IEnumerable<StoredFamilyViewModel>> Handle(GetAllStoredFamily request, CancellationToken cancellationToken)
        {
            var data = await _storedfamilyRepository.GetAll();

            return _mapper.Map<IEnumerable<StoredFamilyViewModel>>(data);
        }
    }
}
