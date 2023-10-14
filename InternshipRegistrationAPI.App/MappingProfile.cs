using InternshipRegistrationAPI.Core.Dtos;
using InternshipRegistrationAPI.Core.Models;
using Models = InternshipRegistrationAPI.Core.Models;

namespace InternshipRegistrationAPI.App
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Program, ProgramDto>().ReverseMap();
            CreateMap<Form, FormDto>().ReverseMap();
            CreateMap<Workflow, WorkflowDto>().ReverseMap();
            CreateMap<Models.Program, ProgramPreviewDto>().ReverseMap();

        }
    }
}