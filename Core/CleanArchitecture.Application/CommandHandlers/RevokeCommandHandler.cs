using AutoMapper;
using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Commands.Auth.Revoke;
using CleanArchitecture.Application.Commons.Rules;
using CleanArchitecture.Domain.Contracts.IRepositories;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.CommandHandlers
{
    public class RevokeCommandHandler : BaseHandler, IRequestHandler<RevokeCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly AuthRules _authRules;

        public RevokeCommandHandler(AuthRules authRules, UserManager<User> userManager, IMapper mapper, IFamilyRepository<FamilyModel> repo, IHttpContextAccessor httpContextAccessor) : base(mapper, repo, httpContextAccessor)
        {
            _userManager = userManager;
            _authRules = authRules;
        }

        public async Task Handle(RevokeCommand request, CancellationToken cancellationToken)
        {
            //find the user based on email
            User user =await _userManager.FindByEmailAsync(request.email);

            await _authRules.EmailShouldNotBeInvalid(user);

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
            return;
        }

    }
}
