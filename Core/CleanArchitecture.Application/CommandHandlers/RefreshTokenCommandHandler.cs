using AutoMapper;
using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Commands.Auth.RefreshToken;
using CleanArchitecture.Application.Commons.Rules;
using CleanArchitecture.Domain.Contracts.IRepositories;
using CleanArchitecture.Domain.Contracts.ITokens;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Domain.Models.AuthResponse;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.CommandHandlers
{
    public class RefreshTokenCommandHandler : BaseHandler,IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly AuthRules _authRules;

        public RefreshTokenCommandHandler(AuthRules authRules,ITokenService tokenService, UserManager<User> userManager, IMapper mapper, IFamilyRepository<FamilyModel> repo, IHttpContextAccessor httpContextAccessor) : base(mapper, repo, httpContextAccessor)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _authRules = authRules;
        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            //retrive the JWT token
            ClaimsPrincipal? principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
            //retrive the email on principal
            string email = principal.FindFirstValue(ClaimTypes.Email);

            //retrive user using by email. Retrive roles using by user
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);

            //validation of refresh token expiry date
            await _authRules.RefreshTokenShouldNotBeExpired(user.RefreshTokenExpiryTime);

            //generate new AccessToken and RefreshToken
            JwtSecurityToken newAccessToken = await _tokenService.CreateToken(user, roles);
            string newRefreshToken = _tokenService.GenerateRefreshToken();

            //update the user
            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new()
            {
                //write JWT token
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };
        }
    }
}
