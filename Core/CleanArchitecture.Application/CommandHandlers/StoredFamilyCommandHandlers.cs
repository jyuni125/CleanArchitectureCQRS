using AutoMapper;
using CleanArchitecture.Application.Commands.Stored;
using CleanArchitecture.Domain.Contracts.IRepositories;
using CleanArchitecture.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.CommandHandlers
{
    public class StoredFamilyCommandHandlers : IRequestHandler<UpdateStoredFamilyCommand, int>,
                                               IRequestHandler<CreateStoredFamilyCommand, Guid>,
                                               IRequestHandler<DeleteStoredFamilyCommand, int>

    {

        private readonly IMapper _mapper;
        private readonly IStoredFamilyRepository<StoredFamilyModel> _repo;


        public StoredFamilyCommandHandlers(IMapper mapper, IStoredFamilyRepository<StoredFamilyModel> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<int> Handle(UpdateStoredFamilyCommand request, CancellationToken cancellationToken)
        {
            return await _repo.Update(request);
        }

        public async Task<Guid> Handle(CreateStoredFamilyCommand request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<StoredFamilyModel>(request);

            return await _repo.Create(data);
        }

        public async Task<int> Handle(DeleteStoredFamilyCommand request, CancellationToken cancellationToken)
        {
            return await _repo.Delete(request.id);
        }
    }
}
