using AutoMapper;
using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Commands.Auth.Login;
using CleanArchitecture.Application.Commons.Rules;
using CleanArchitecture.Domain.Contracts.IRepositories;
using CleanArchitecture.Domain.Contracts.IServices.IServices;
using CleanArchitecture.Domain.Contracts.ITokens;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Domain.Models.AuthResponse;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CleanArchitecture.Application.CommandHandlers
{
    public class LoginCommandHandler : BaseHandler,
                                       IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly AuthRules _authrules;
        private readonly UserManager<User> _usermanager;
        private readonly RoleManager<Role> _rolemanager;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly IdentityOptions _options;


        public LoginCommandHandler(IOptions<IdentityOptions> options, IConfiguration configuration,ITokenService tokenService,AuthRules authRules,UserManager<User> userManager,RoleManager<Role> roleManager,IMapper mapper, IFamilyRepository<FamilyModel> repo, IHttpContextAccessor httpContextAccessor) : base(mapper, repo, httpContextAccessor)
        {
            _options = options.Value;
            _authrules = authRules;
            _usermanager = userManager;
            _rolemanager = roleManager;
            _configuration = configuration;
            _tokenService = tokenService;
    
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            //get user by email
            User user = await _usermanager.FindByEmailAsync(request.Email);
   
            //return true if the password of email and the password input is equal
            bool checkPassword = await _usermanager.CheckPasswordAsync(user, request.Password);


            //check if user is null or password is not the same
            await _authrules.EmailorPasswordShouldNotBeInvalid(user, checkPassword);

            //----------------------------------------------------------------------------------------------------
            //checck if the option for comfirmed email is true and if its true, check the user if verified
            bool RequiredComfirmEmail = _options.SignIn.RequireConfirmedEmail;
            bool IfEmailIsConfirmed = await _usermanager.IsEmailConfirmedAsync(user);

            _authrules.EmailShouldBeVerified(RequiredComfirmEmail, IfEmailIsConfirmed);
            //----------------------------------------------------------------------------------------------------
                



            //get roles
            IList<string> roles = await _usermanager.GetRolesAsync(user);

            //create and get refreshtoken
            JwtSecurityToken token = await _tokenService.CreateToken(user, roles);
            string refreshToken = _tokenService.GenerateRefreshToken();

            //get the config values on JSON and put on a property
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            //input value of refreshToken and validity in User
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

            //update for user
            await _usermanager.UpdateAsync(user);
            await _usermanager.UpdateSecurityStampAsync(user);

            string _token = new JwtSecurityTokenHandler().WriteToken(token);

            await _usermanager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", _token);

            return new()
            {
                Token = _token,
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            };
        }
    }
}
