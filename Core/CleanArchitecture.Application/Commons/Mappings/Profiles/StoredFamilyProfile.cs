using AutoMapper;
using CleanArchitecture.Application.DTOs.Family;
using CleanArchitecture.Application.Queries.Stored;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Commons.Mappings.Profiles
{
    public class StoredFamilyProfile : Profile
    {
        public StoredFamilyProfile()
        {

            CreateMap<StoredFamilyModel, StoredEntity>();



            CreateMap<GetStoredAllFamilyByGenderDTO, GetAllStoredFamilyByGenderQuery>();

            CreateMap<StoredEntity, StoredFamilyModel>()
                .ConstructUsing(data => new StoredFamilyModel(data.Firstname, data.Lastname, data.Gender, data.status));
            CreateMap<StoredFamilyModel, StoredFamilyViewModel>();
        }
    }
}
