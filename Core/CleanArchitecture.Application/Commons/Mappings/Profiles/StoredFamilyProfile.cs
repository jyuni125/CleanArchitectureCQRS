using AutoMapper;
using CleanArchitecture.Application.Commands.Stored;
using CleanArchitecture.Application.DTOs.Stored;
using CleanArchitecture.Application.Queries.Stored;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enumerations;
using CleanArchitecture.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CleanArchitecture.Application.Commons.Mappings.Profiles
{
    public class StoredFamilyProfile : Profile
    {
        public StoredFamilyProfile()
        {



            //-----------------CREATE----------------------

            CreateMap<CreateStoredFamilyDTO, CreateStoredFamilyCommand>();
            CreateMap<CreateStoredFamilyCommand, StoredFamilyModel>()
                    .ConstructUsing(data => new StoredFamilyModel(data.Firstname, data.Lastname, data.Gender, data.status));
            CreateMap<StoredFamilyModel, StoredEntity>();
                    //.ForMember(data => data.Gender,
                    //    dataGender => dataGender.MapFrom(d => (short)d.Gender));




            //----------------UPDATE-----------------------

            CreateMap<UpdateStoredFamilyDTO, UpdateStoredFamilyCommand>();


            //---------------DELETE------------------------

            CreateMap<DeleteStoredFamilyDTO, DeleteStoredFamilyCommand>();



            //-------------------READ----------------------
            CreateMap<GetStoredAllFamilyByGenderDTO, GetAllStoredFamilyByGenderQuery>();
            CreateMap<GetStoredFamilyByIdDTO, GetStoredFamilyByIdQuery>();

            CreateMap<StoredEntity, StoredFamilyModel>()
                  .ForMember(data => data.Gender,
                        dataGender => dataGender.MapFrom(d =>(short)convert2(d.Gender)))
                .ConstructUsing(data => new StoredFamilyModel(data.Firstname, data.Lastname, (short)convert2(data.Gender), data.status));
            CreateMap<StoredFamilyModel, StoredFamilyViewModel>();
        }


    

        public static Gender convert2(string data)
        {

           return (Gender)Enum.Parse(typeof(Gender), data,true);
          
        }

    }

}
