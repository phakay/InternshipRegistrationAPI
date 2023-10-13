using AutoMapper;
using InternshipRegistrationAPI.Core.Dtos;
using InternshipRegistrationAPI.Core.Models;
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