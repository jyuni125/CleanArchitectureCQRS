using AutoMapper;
using CleanArchitecture.Application.Commands.Family;
using CleanArchitecture.Application.Commons.Results;
using CleanArchitecture.Application.DTOs.Family;
using CleanArchitecture.Application.Queries.Family;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Contracts.IServices;
using CleanArchitecture.Domain.Contracts.IServices.IServicesFacades;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace CleanArchitecture.WebApi.Controllers
{
    public class FamilyController : BaseController
    {
       

        public FamilyController(IMediator mediator, IMapper mapper, IFamilyServices<FamilyModel> service) : base(mediator, mapper)
        {
            
        }

        [HttpGet]
        public async Task<IActionResult> Getall()
        {
             return await Handle<IEnumerable<FamilyViewModel>, GetAllFamilyQuery>(new GetAllFamilyQuery());
  
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> GetById([FromQuery] GetFamilyByIdDTO dto)
        {
             return await Handle<GetFamilyByIdDTO, GetAllFamilyByIdQuery,FamilyViewModel > (dto);

        }


        [HttpGet]
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
