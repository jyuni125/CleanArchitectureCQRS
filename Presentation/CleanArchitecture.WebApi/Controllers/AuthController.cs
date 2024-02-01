using AutoMapper;
using CleanArchitecture.Application.Commands.Auth.Register;
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
        public async Task<IActionResult> Register([FromBody]RegisterCommand com)
        {

            return await Handle<Unit, RegisterCommand>(com);
            
        }
    }
}
