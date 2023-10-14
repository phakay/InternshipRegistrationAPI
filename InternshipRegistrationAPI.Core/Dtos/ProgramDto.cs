using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InternshipRegistrationAPI.Core.Models;

namespace InternshipRegistrationAPI.Core.Dtos
{
    public class ProgramDto 
    {
        public string Id { get; set; }  
        [Required]
        public string Type { get; set; }
        [Required]
        public string ProgramTitle { get; set; }
        public string ProgramSummary { get; set; }
        [Required]
        public string ProgramDescription { get; set; }
        public List<string> SkillsRequiredForProgram { get; set; }
        public string ProgramBenefits { get; set; }
        public string ApplicationCriteria { get; set; }
        [Required]
        public AdditionalProgramInformation AdditionalProgramInformation { get; set; }

        public static ProgramDto LoadSampleData()
        {
            ProgramDto programDto = new ProgramDto
            {
                Id = "001",
                Type = "program",
                ProgramDescription = "Test Descritpion",
                ProgramTitle = "Title",
                AdditionalProgramInformation = new AdditionalProgramInformation()
                {
                    ProgramType = ProgramType.FullTime,
                    ProgramLocation = new Location()
                    {
                        City = "London",
                        CountryShortName = "UK",
                        FullyRemote = true
                    }
                }
            };

            return programDto;
        }
    }
}
