using AutoMapper;
using CleanArchitecture.Application.Commands.Family;
using CleanArchitecture.Application.Commons.Results;
using CleanArchitecture.Application.DTOs.Family;
using CleanArchitecture.Application.Queries.Family;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Contracts.IServices;
using CleanArchitecture.Domain.Contracts.IServices.IServices;
using CleanArchitecture.Domain.Contracts.IServices.IServicesFacades;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Services.Poco;
using CleanArchitecture.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace CleanArchitecture.WebApi.Controllers
{
    public class FamilyController : BaseController
    {
        private readonly IEmailSender _emailsender;

        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly string userId;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IdentityOptions Options;
       // private readonly SignInManager<User> _signinManager;

        public FamilyController(IOptions<IdentityOptions> _options, IConfiguration configuration, UserManager<User> userManager,IMediator mediator, IMapper mapper, IHttpContextAccessor httpContextAccessor ,IFamilyServices<FamilyModel> service,IEmailSender emailsender) : base(mediator, mapper)//SignInManager<User> signinManager,
        {
            Options = _options.Value;
            _emailsender = emailsender;
            _httpContextAccessor = httpContextAccessor;
            userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _userManager = userManager;
            _configuration = configuration;
           //_signinManager = signinManager;
        }

        [HttpGet]
        [Authorize(Roles ="Admin,user")]
        public async Task<IActionResult> Getall()
        {


            /*
            await _emailsender.send(new[] 
                                    {
                                        "arbertamaro@gmail.com"
                                     },
                                    "Sample Subject",
                                    "Sample email body");


            await _emailsender.send(new[]
                                     {
                                        "arbertamaro@gmail.com"
                                     },
                                      "Sample Subject",
                                      "Sample <h1>email</h1> body",
                                      true,
                                      "Amaro_Arbert_CV.pdf",
                                      System.IO.File.ReadAllBytes("C:/Users/arber/Desktop/RESUME/Amaro_Arbert_CV.pdf"));
          

            return Ok("Email Sent");
              */
            User uyer = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email));
            //bool x =await _signinManager.CanSignInAsync(data);
            bool y = await _userManager.IsEmailConfirmedAsync(uyer);

            bool z = Options.SignIn.RequireConfirmedEmail;


            //return Ok(Options.SignIn.RequireConfirmedEmail+" : "+y);
            /*
            if (z)
                return await Handle<IEnumerable<FamilyViewModel>, GetAllFamilyQuery>(new GetAllFamilyQuery());
            else
                return Ok("Please Confirm Email First");
            */
            
            User data = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email));
    



            var name = data.Fullname;


            var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(data);
            var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
            string url = $"{_configuration["JWT:Issuer"]}/api/Family/confirmEmail?userId={data.Id}&token={validEmailToken}";

            // return Ok((confirmEmailToken is not null)?confirmEmailToken.ToString():"not found");

            var ressult =await _userManager.ConfirmEmailAsync(data, confirmEmailToken);

            return Ok(ressult);
            
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> GetById([FromQuery] GetFamilyByIdDTO dto)
        {
             return await Handle<GetFamilyByIdDTO, GetAllFamilyByIdQuery,FamilyViewModel > (dto);

        }


        [HttpGet]
        [Authorize(Roles = "user")]
        [Route("ByLastname")]
        public async Task<IActionResult> GetByLastname([FromQuery] GetAllFamilyByLastnameDTO dto)
        {
            return await Handle<GetAllFamilyByLastnameDTO, GetAllFamilyByLastnameQuery,IEnumerable<FamilyViewModel>> (dto);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFamilyDTO dto)
        {
            return await Handle<CreateFamilyDTO, CreateFamilyCommand, Guid>(dto);

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateFamilyDTO dto)
        {
            return await Handle<UpdateFamilyDTO, UpdateFamilyCommand, int>(dto);

        }

        [HttpPut]
        [Route("UpdateLastname")]
        public async Task<IActionResult> UpdateLastname([FromBody] UpdateByLastnameFamilyDTO dto)
        {
            return await Handle<UpdateByLastnameFamilyDTO, UpdateByLastnameFamilyCommand, Unit>(dto);

        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteFamilyDTO dto)
        {
            return await Handle<DeleteFamilyDTO, DeleteFamilyCommand, int>(dto);

        }


        [HttpDelete("byId/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            DeleteFamilyDTO dto = new DeleteFamilyDTO();

            dto.Id = id;

            return await Handle<DeleteFamilyDTO, DeleteFamilyCommand, int>(dto);

        }

    }
}
