using AutoMapper;
using CleanArchitecture.Application.Commands.Family;
using CleanArchitecture.Application.DTOs.Family;
using CleanArchitecture.Application.Queries.Family;
using CleanArchitecture.Application.Queries.Stored;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers
{

    public class StoredController : BaseController
    {
        public StoredController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }


        [HttpGet]
        public async Task<IActionResult> getallfromstoredview()
        {
            return await Handle<IEnumerable<StoredFamilyViewModel>, GetAllStoredFamilyQuery>(new GetAllStoredFamilyQuery());

        }


        [HttpGet]
        [Route("GetByGender")]
        public async Task<IActionResult> storedProcedureForGettingallFamilyByGender([FromQuery]GetStoredAllFamilyByGenderDTO dto)
        {
            return await Handle<GetStoredAllFamilyByGenderDTO,GetAllStoredFamilyByGenderQuery, IEnumerable<StoredFamilyViewModel>>(dto);

        }
    }
}
