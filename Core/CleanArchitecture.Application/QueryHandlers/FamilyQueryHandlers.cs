using AutoMapper;
using CleanArchitecture.Application.Queries.Family;
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
    public class FamilyQueryHandlers : IRequestHandler<GetAllFamilyQuery, IEnumerable<FamilyViewModel>>,
                                       IRequestHandler<GetAllFamilyByIdQuery, FamilyViewModel>,
                                       IRequestHandler<GetAllFamilyByLastnameQuery, IEnumerable<FamilyViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IFamilyRepository<FamilyModel> _familyRepository;
        public FamilyQueryHandlers(IMapper mapper, IFamilyRepository<FamilyModel> familyRepository)
        {
            _mapper = mapper;
            _familyRepository = familyRepository;
        }

        public async Task<IEnumerable<FamilyViewModel>> Handle(GetAllFamilyQuery request, CancellationToken cancellationToken)
        {
            var data = await _familyRepository.GetAll();

            return _mapper.Map<IEnumerable<FamilyViewModel>>(data);

        }

        public async Task<FamilyViewModel> Handle(GetAllFamilyByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _familyRepository.GetOne(request.Id);

            return _mapper.Map<FamilyViewModel>(data);
        }

        public async Task<IEnumerable<FamilyViewModel>> Handle(GetAllFamilyByLastnameQuery request, CancellationToken cancellationToken)
        {
            var data = await _familyRepository.getByLastName(request.Lastname);

            return _mapper.Map<IEnumerable<FamilyViewModel>>(data);
        }
    }
}
