using AutoMapper;
using CleanArchitecture.Domain.Contracts.IRepositories;
using CleanArchitecture.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Bases
{
    public class BaseHandler
    {
        protected readonly IMapper _mapper;
        protected readonly IFamilyRepository<FamilyModel> _repo;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly string userId;

        public BaseHandler(IMapper mapper, IFamilyRepository<FamilyModel> repo,IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _repo = repo;
            _httpContextAccessor = httpContextAccessor;
            userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        }
    }
}
