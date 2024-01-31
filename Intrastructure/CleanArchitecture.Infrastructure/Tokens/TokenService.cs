using CleanArchitecture.Domain.Contracts.ITokens;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Tokens
{
    public class TokenServices : ITokenService
    {
        private readonly UserManager<User> _usermanager;
        private readonly TokenSettings _tokenssettings;


        public TokenServices(IOptions<TokenSettings> options,UserManager<User> userManager)
        {
            _usermanager = userManager;
            _tokenssettings = options.Value;
        }


        public async Task<JwtSecurityToken> CreateToken(User user, IList<string> roles)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email)
            };


            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenssettings.Secret));

            var token = new JwtSecurityToken(
                issuer: _tokenssettings.Issuer,
                audience: _tokenssettings.Audience,
                expires: DateTime.Now.AddMinutes(_tokenssettings.TokenValidityInMinutes),
                claims : claims,
                signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256)
                );


            await _usermanager.AddClaimsAsync(user, claims);

            return token;
        }

        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken()
        {
            throw new NotImplementedException();
        }
    }
}
