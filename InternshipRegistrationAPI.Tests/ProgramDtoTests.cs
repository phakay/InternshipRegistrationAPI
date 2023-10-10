using System.ComponentModel.DataAnnotations;
using InternshipRegistrationAPI.Core.Dtos;
using InternshipRegistrationAPI.Core.Utils;
using Xunit;

namespace InternshipRegistrationAPI.Tests
{
    public class ProgramDtoTests
    {

        [Fact]
        public void ValidateModel_ProgramDto_ValidData_Success()
        {
            ProgramDto programDto = ProgramDto.LoadSampleData();
            List<ValidationResult>? results = DataModelHelper.DoValidationCheck(programDto);
            Assert.NotNull(results);
            Assert.False(results.Count > 0);
        }
        
        [Fact]
        public void ValidateModel_ProgramDto_ProgramTitle_NotSet_Failure()
        {
            ProgramDto programDto = ProgramDto.LoadSampleData();
            programDto.Data.ProgramTitle = "";
            List<ValidationResult> results = DataModelHelper.DoValidationCheck(programDto);
            Assert.NotNull(results);
            Assert.True(results.Count > 0);
        }
        
        [Fact]
        public void ValidateModel_ProgramDto_ProgramTitle_NotSet_ErrorMessageContainsPropertyName()
        {
            ProgramDto programDto = ProgramDto.LoadSampleData();
            programDto.Data.ProgramTitle = "";
            List<ValidationResult>? results = DataModelHelper.DoValidationCheck(programDto);
            Assert.NotNull(results);
            bool errorMessageHasPropertyName =
                results.Exists(res => res.ErrorMessage != null && res.ErrorMessage.Contains(nameof(programDto.Data.ProgramTitle)));
            Assert.True(errorMessageHasPropertyName);
        }

        [Fact]
        public void ValidateModel_ProgramDto_ProgramType_NotSet_ErrorMessageContainsPropertyName()
        {
            ProgramDto programDto = ProgramDto.LoadSampleData();
            programDto.Data.AdditionalProgramInformation.ProgramType = null;
            List<ValidationResult>? results = DataModelHelper.DoValidationCheck(programDto);
            Assert.NotNull(results);
            bool errorMessageHasPropertyName =
                results.Exists(res => res.ErrorMessage != null && res.ErrorMessage
                    .Contains(nameof(programDto.Data.AdditionalProgramInformation.ProgramType)));
            Assert.True(errorMessageHasPropertyName);
        }
        
        [Fact]
        public void ValidateModel_ProgramDto_ProgramType_NotSet_Failure()
        {
            ProgramDto programDto = ProgramDto.LoadSampleData();
            programDto.Data.AdditionalProgramInformation.ProgramType = null;
            List<ValidationResult> results = DataModelHelper.DoValidationCheck(programDto);
            Assert.NotNull(results);
            Assert.True(results.Count > 0);
        }
        
    }
}