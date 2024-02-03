using AutoMapper;
using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Commands.Auth.Login;
using CleanArchitecture.Application.Commons.Rules;
using CleanArchitecture.Domain.Contracts.IRepositories;
using CleanArchitecture.Domain.Contracts.ITokens;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Domain.Models.AuthResponse;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public LoginCommandHandler(IConfiguration configuration,ITokenService tokenService,AuthRules authRules,UserManager<User> userManager,RoleManager<Role> roleManager,IMapper mapper, IFamilyRepository<FamilyModel> repo, IHttpContextAccessor httpContextAccessor) : base(mapper, repo, httpContextAccessor)
        {
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
