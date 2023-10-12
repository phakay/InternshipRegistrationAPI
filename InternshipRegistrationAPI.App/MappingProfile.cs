using AutoMapper;
using InternshipRegistrationAPI.Core.Dtos;
using InternshipRegistrationAPI.Data.DataModels;

namespace InternshipRegistrationAPI.App
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProgramDto, ProgramData>().ReverseMap();
            
        }
    }
}