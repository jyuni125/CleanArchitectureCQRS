using AutoMapper;
using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Commands.Auth.Revoke;
using CleanArchitecture.Domain.Contracts.IRepositories;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.CommandHandlers
{
    public class RevokeAllCommandHandler : BaseHandler, IRequestHandler<RevokeAllCommand>
    {
        private readonly UserManager<User> _userManager;

        public RevokeAllCommandHandler(UserManager<User> userManager, IMapper mapper, IFamilyRepository<FamilyModel> repo, IHttpContextAccessor httpContextAccessor) : base(mapper, repo, httpContextAccessor)
        {
            _userManager = userManager;
        }

        public async Task Handle(RevokeAllCommand request, CancellationToken cancellationToken)
        {
            List<User> users = await _userManager.Users.ToListAsync(cancellationToken);
           
            foreach(User user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            return;
        }
    }
}
