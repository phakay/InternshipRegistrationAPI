using AutoMapper;
using InternshipRegistrationAPI.Core.Dtos;
using Models = InternshipRegistrationAPI.Core.Models;

namespace InternshipRegistrationAPI.App
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Program, ProgramDto>().ReverseMap();
            CreateMap<Models.Program, ProgramPutDto>().ReverseMap();
        }
    }
}