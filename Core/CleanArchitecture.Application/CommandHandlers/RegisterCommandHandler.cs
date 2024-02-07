using AutoMapper;
using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Commands.Auth.Register;
using CleanArchitecture.Application.Commons.Exceptions;
using CleanArchitecture.Application.Commons.Rules;
using CleanArchitecture.Domain.Contracts.IRepositories;
using CleanArchitecture.Domain.Contracts.IServices.IServices;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CleanArchitecture.Application.CommandHandlers
{
    public class RegisterCommandHandler : BaseHandler,
                                          IRequestHandler<RegisterCommand,string>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly AuthRules _authRules;
        private readonly IdentityOptions _options;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;

        public RegisterCommandHandler(IConfiguration configuration,IEmailSender emailSender,IOptions<IdentityOptions> options, AuthRules authRules,RoleManager<Role> roleManager,UserManager<User> userManager,IMapper mapper, IFamilyRepository<FamilyModel> repo, IHttpContextAccessor httpContextAccessor) : base(mapper, repo, httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authRules = authRules;
            _options = options.Value;
            _emailSender = emailSender;
            _configuration = configuration;

        }



        
        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
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

            //-----------------------------------------------------------------------------------------------------------
            //check if the Authentication option for confirm email is true
            var confirmEmailOption = _options.SignIn.RequireConfirmedEmail;
            User checkuser = await _userManager.FindByEmailAsync(request.Email);

            //get the token that we need on verification
            var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(checkuser);

            //convert the token into url encode
            string code = HttpUtility.UrlEncode(confirmEmailToken);
       

            //setup url with path,user Id and User token
            string url = $"{_configuration["JWT:Issuer"]}/api/Auth/confirmEmail?userId={checkuser.Id}&&token={code}";

            string returnstring = "";

            //if the option on authentication is true, send email
            if (confirmEmailOption)
            {
                _emailSender.send(new[] { request.Email },
                                  "Email Verification",
                                  $"Hello <b>{request.Fullname}</b><br/><br/>" +
                                  $"<p>Pls click \"Confirm Here\" to verify your Email.<a href=\"{url}\">Confirm Here</a>",
                                  true,
                                  null, null);

                returnstring = "PLEASE VERIFY FIRST YOUR ACCOUNT IN EMAIL BEFORE LOGIN  ";
            }
            //-----------------------------------------------------------------------------------------------------------------


           return returnstring;
        }

    }
}
