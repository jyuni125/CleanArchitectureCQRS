using AutoMapper;
using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Commands.Auth.ConfirmEmail;
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
using System.Web;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CleanArchitecture.Application.CommandHandlers
{
    public class ConfirmEmailCommandHandler : BaseHandler, IRequestHandler<ConfirmEmailCommand, string>
    {
        private readonly UserManager<User> _usermanager;

        public ConfirmEmailCommandHandler(UserManager<User> usermanager, IMapper mapper, IFamilyRepository<FamilyModel> repo, IHttpContextAccessor httpContextAccessor) : base(mapper, repo, httpContextAccessor)
        {
            _usermanager = usermanager;
        }

        public async Task<string> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            User user = await _usermanager.FindByIdAsync(request.userId);

            string decoded = HttpUtility.UrlDecode(request.token);

            string trimdecoded = decoded.Replace(" ", "+");

            var result = await _usermanager.ConfirmEmailAsync(user, trimdecoded);

          
            if(result.Succeeded)
            {
                return "Your email has been verified";
            }

            return result.ToString();

            
        }
    }
}
