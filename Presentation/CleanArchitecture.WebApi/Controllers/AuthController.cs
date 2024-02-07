using AutoMapper;
using CleanArchitecture.Application.Commands.Auth.ConfirmEmail;
using CleanArchitecture.Application.Commands.Auth.Login;
using CleanArchitecture.Application.Commands.Auth.RefreshToken;
using CleanArchitecture.Application.Commands.Auth.Register;
using CleanArchitecture.Application.Commands.Auth.Revoke;
using CleanArchitecture.Domain.Models.AuthResponse;
using CleanArchitecture.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.WebApi.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IdentityOptions _options;

        public AuthController(IOptions<IdentityOptions> options, IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
            _options = options.Value;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterCommand com)
        {
            /*
            var confirmEmailOption = _options.SignIn.RequireConfirmedEmail;
            string forEmail="";

            if (confirmEmailOption)
                forEmail = "PLEASE VERIFY FIRST YOUR ACCOUNT IN EMAIL BEFORE LOGIN";
            */
            return await Handle<string, RegisterCommand>(com);
            
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

        //[HttpGet("confirmEmail/{userId}&{token}")]
        //[HttpGet("confirmEmail")]
        [HttpGet]
        [Route("confirmEmail")]
        public async Task<IActionResult> EmailVerification(string userId, string token)
        {
            //string userIdtrim = userId.Replace("userId=", "");
            //string tokentrim = token.Replace("token=", "");

           // return Ok("userId : " + userId + "  |  " + "Token : " + token+"");

            return await Handle<string, ConfirmEmailCommand>(new ConfirmEmailCommand { userId= userId, token= token });

        }
    }
}
