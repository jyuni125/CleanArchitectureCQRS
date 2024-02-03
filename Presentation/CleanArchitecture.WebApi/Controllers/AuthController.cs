using AutoMapper;
using CleanArchitecture.Application.Commands.Auth.Login;
using CleanArchitecture.Application.Commands.Auth.RefreshToken;
using CleanArchitecture.Application.Commands.Auth.Register;
using CleanArchitecture.Application.Commands.Auth.Revoke;
using CleanArchitecture.Domain.Models.AuthResponse;
using CleanArchitecture.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers
{
    public class AuthController : BaseController
    {
        
        public AuthController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
            
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterCommand com)
        {

            return await Handle<Unit, RegisterCommand>(com);
            
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand com)
        {
            return await Handle<LoginResponse, LoginCommand>(com);

        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand com)
        {
            return await Handle<RefreshTokenResponse, RefreshTokenCommand>(com);

        }

        [HttpPost]
        [Route("Revoke")]
        public async Task<IActionResult> Revoke([FromQuery] RevokeCommand com)
        {
            return await Handle<Unit, RevokeCommand>(com);

        }

        [HttpPost]
        [Route("RevokeAll")]
        public async Task<IActionResult> RevokeAll()
        {
            return await Handle<Unit, RevokeAllCommand>(new RevokeAllCommand());

        }
    }
}
