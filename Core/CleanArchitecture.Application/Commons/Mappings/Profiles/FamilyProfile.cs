using AutoMapper;
using CleanArchitecture.Application.Commands.Family;
using CleanArchitecture.Application.DTOs.Family;
using CleanArchitecture.Application.Queries.Family;
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
    public class FamilyProfile : Profile
    {
        public FamilyProfile()
        {

            //CREATE
            CreateMap<CreateFamilyDTO, CreateFamilyCommand>();
            CreateMap<CreateFamilyCommand, FamilyModel>()
                    .ConstructUsing(data => new FamilyModel(data.status, data.FirstName, data.LastName, data.Gender));
            CreateMap<FamilyModel, Family>()
                    .ForMember(data => data.Gender,
                                    genderInside => genderInside.MapFrom(
                                        genderData => (short)genderData.Gender
                                        ));

            // dto -> command   (baseController)
            // command -> model (ComamndHandler)
            // model -> entity  (Repository)
            // RETURN ID



            //UPDATE
            CreateMap<UpdateFamilyDTO, UpdateFamilyCommand>();
            CreateMap<UpdateByLastnameFamilyDTO, UpdateByLastnameFamilyCommand>();
            //dto -> command    (baseController)
            //RETURN null




            //DELETE
            CreateMap<DeleteFamilyDTO, DeleteFamilyCommand>();

            //dto -> command    (baseController)
            //RETURN null


            //READ
            CreateMap<GetAllFamilyByLastnameDTO, GetAllFamilyByLastnameQuery>();
            CreateMap<GetFamilyByIdDTO, GetAllFamilyByIdQuery>();
            CreateMap<Family, FamilyModel>()
                  .ConstructUsing(data => new FamilyModel(data.Id, data.FirstName, data.LastName, data.Gender));
            CreateMap<FamilyModel, FamilyViewModel>();

            //entity -> Model   (Repository)
            //Model -> ViewModel(QueryHandler)




        }
    }
}
