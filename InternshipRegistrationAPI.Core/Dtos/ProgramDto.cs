using System.ComponentModel.DataAnnotations;
using InternshipRegistrationAPI.Core.Models;

namespace InternshipRegistrationAPI.Core.Dtos
{
 
    public class ProgramDto
    {
        public string Id { get; set; }
        [Required]
        public string ProgramTitle { get; set; }
        public string ProgramSummary { get; set; }
        [Required]
        public string ProgramDescription { get; set; }
        public string SkillsRequiredForProgram { get; set; }
        public string ProgramBenefits { get; set; }
        public string ApplicationCriteria { get; set; }
        [Required]
        public AdditionalProgramInformation AdditionalProgramInformation { get; set; }
    }
    public class ProgramPutDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string ProgramTitle { get; set; }
        public string ProgramSummary { get; set; }
        [Required]
        public string ProgramDescription { get; set; }
        public string SkillsRequiredForProgram { get; set; }
        public string ProgramBenefits { get; set; }
        public string ApplicationCriteria { get; set; }
        [Required]
        public AdditionalProgramInformation AdditionalProgramInformation { get; set; }

    }
}
