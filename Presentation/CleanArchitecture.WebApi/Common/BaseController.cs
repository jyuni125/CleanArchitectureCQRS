using AutoMapper;
using CleanArchitecture.Application.Commons.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly IMapper _mapper;
        protected readonly IMediator _mediator;

        public BaseController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        protected async Task<IActionResult> Handle<T1, T2, T3>(T1 dto)
        {

            var queryOrCommmand = _mapper.Map<T2>(dto);

            return await Handle<T3, T2>(queryOrCommmand);

        }




        protected async Task<IActionResult> Handle<T1, T2>(T2 queryOrCommmand)

        {

            if (queryOrCommmand == null)
                return BadRequest();

            var result = new QueryOrCommandResult<T1>();
            if (ModelState.IsValid)
            {
                try
                {
                    var returndata = await _mediator.Send(queryOrCommmand);
                    result.Data = (T1?)returndata;
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.Messages.Add(ex.Message);
                }
            }
            else
            {
                result.Messages = ModelState.Values.SelectMany(m => m.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
            }

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);

        }

    }
}
