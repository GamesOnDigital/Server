using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DTO.Classes
{
    public class CountDProfile : Profile
    {
        public CountDProfile()
        {
            CreateMap<DAL.Models.Audience, Audience>().ReverseMap();
            CreateMap< Audience, DAL.Models.Audience>().ReverseMap();
            CreateMap<DAL.Models.Game, Game>().ReverseMap();
            CreateMap<DAL.Models.Gender, Gender>().ReverseMap();
            CreateMap<DAL.Models.HowKnown, HowKnown>().ReverseMap();
            CreateMap<DAL.Models.Setting, Settings>().ReverseMap();
            CreateMap<DAL.Models.TypeGame, TypeGame>().ReverseMap();

            CreateMap<DAL.Models.User, User>().ReverseMap();

            //CreateMap<DAL.Models.User, User>()
            //    .ForMember(dest => dest.Name,
            //    opt => opt.MapFrom(src => src.Details.FirstName)).ReverseMap();


        }
    }
}
