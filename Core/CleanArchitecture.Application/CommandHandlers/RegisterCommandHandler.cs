using AutoMapper;
using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Commands.Auth.Register;
using CleanArchitecture.Application.Commons.Exceptions;
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
    public class RegisterCommandHandler : BaseHandler,
                                          IRequestHandler<RegisterCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly AuthRules _authRules;

        public RegisterCommandHandler(AuthRules authRules,RoleManager<Role> roleManager,UserManager<User> userManager,IMapper mapper, IFamilyRepository<FamilyModel> repo, IHttpContextAccessor httpContextAccessor) : base(mapper, repo, httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authRules = authRules;
        }



        
        public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
        {

          

            User theuser = await _userManager.FindByEmailAsync(request.Email);

            //if (theuser is not null) throw new UserAlreadyExistException();

            _authRules.UserShouldNotExist(theuser);

            User user = _mapper.Map<User>(request);


           user.UserName = request.Email;
           user.SecurityStamp = Guid.NewGuid().ToString();

           //create users
           IdentityResult result = await _userManager.CreateAsync(user, request.Password);

           //create Roles
           if(result.Succeeded)
           {
               if(!await _roleManager.RoleExistsAsync("user"))
                   await _roleManager.CreateAsync(new Role
                   {
                       Id= Guid.NewGuid(),
                       Name = "user",
                       NormalizedName = "USER",
                       ConcurrencyStamp = Guid.NewGuid().ToString(),
                   });
               await _userManager.AddToRoleAsync(user, "user");
           }

           return;
        }

    }
}
