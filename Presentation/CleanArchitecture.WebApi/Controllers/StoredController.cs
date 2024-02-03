using AutoMapper;
using CleanArchitecture.Application.Commands.Family;
using CleanArchitecture.Application.Commands.Stored;
using CleanArchitecture.Application.DTOs.Stored;
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

        [HttpGet]
        [Route("GetByid")]
        public async Task<IActionResult> storedProcedureForGettingallFamilyById([FromQuery] GetStoredFamilyByIdDTO dto)
        {
            return await Handle<GetStoredFamilyByIdDTO, GetStoredFamilyByIdQuery, StoredFamilyViewModel>(dto);

        }


        [HttpPut]
        public async Task<IActionResult> updateByStoredProcedure([FromBody] UpdateStoredFamilyDTO dto)
        {
            return await Handle<UpdateStoredFamilyDTO, UpdateStoredFamilyCommand, int>(dto);

        }

        [HttpPost]
        public async Task<IActionResult> CreateStoredProcedure([FromQuery] CreateStoredFamilyDTO dto)
        {
            return await Handle<CreateStoredFamilyDTO, CreateStoredFamilyCommand, Guid>(dto);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStoredProcedure([FromQuery] DeleteStoredFamilyDTO dto)
        {
            return await Handle<DeleteStoredFamilyDTO, DeleteStoredFamilyCommand, int>(dto);

        }
    }
}
