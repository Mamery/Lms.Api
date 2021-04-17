using AutoMapper;
using Lms.Core.Dto;
using Lms.Core.Entities;

namespace Lms.Api
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        { //createAutomap
          //CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, CourseDto>()
                .ForMember(
                endDate => endDate.EndDate,
                opt => opt.MapFrom(src => $"{src.StartDate.AddMonths(3)}")).ReverseMap();
            // CreateMap<Module, ModuleDto>().ReverseMap();



            CreateMap<Module, ModuleDto>()
                .ForMember(
                endDate => endDate.EndDate,
                opt => opt.MapFrom(src => $"{src.StartDate.AddMonths(1)}")).ReverseMap();
        }
       


    }
}