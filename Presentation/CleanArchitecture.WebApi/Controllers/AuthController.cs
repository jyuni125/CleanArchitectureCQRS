using AutoMapper;
using CleanArchitecture.Application.Commands.Auth.Login;
using CleanArchitecture.Application.Commands.Auth.Register;
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
    }
}
