using InternshipRegistrationAPI.Core.Models;

namespace InternshipRegistrationAPI.Core.Dtos
{
    public class ProgramDto
    {
        public Program Data { get; set; }
        public static ProgramDto LoadSampleData()
        {
            ProgramDto programDto = new ProgramDto()
            {
                Data = new Program
                {
                    Id = "",
                    ProgramTitle = "Sample Title",
                    ProgramSummary = "",
                    ProgramDescription = "Sample Description",
                    SkillsRequiredForProgram = "",
                    ProgramBenefits = "",
                    ApplicationCriteria = "",
                    AdditionalProgramInformation = new AdditionalProgramInformation
                    {
                        ProgramType = ProgramType.FullTime,
                        ProgramStart = default,
                        ApplicationOpen = default,
                        ApplicationClose = default,
                        Duration = new Duration
                        {
                            Value = 1,
                            Unit = DurationUnit.Day
                        },
                        ProgramLocation = new Location
                        {
                            CountryShortName = "UK",
                            City = "London",
                            FullyRemote = false
                        },
                        MinimumQualification = Qualification.HighSchool,
                        MaxNumberOfApplication = 0
                    }
                }
            };
            return programDto;
        }
    }
}
