using System.Web.Http.Results;
using AutoMapper;
using InternshipRegistrationAPI.App;
using InternshipRegistrationAPI.App.Controllers;
using InternshipRegistrationAPI.Core.Dtos;
using InternshipRegistrationAPI.Core.Exceptions;
using InternshipRegistrationAPI.Data.Contracts;
using InternshipRegistrationAPI.Data.DataModels;
using Moq;
using Xunit;

namespace InternshipRegistrationAPI.Tests;

public class ProgramControllerTests
{
    private Mock<IProgramRepository> _mockProgramRepository;
    private ProgramController _programController;

    public ProgramControllerTests()
    {
        IMapper mapper = new MapperConfiguration(c => c.AddProfile<MappingProfile>()).CreateMapper();
        _mockProgramRepository = new Mock<IProgramRepository>();
        _programController = new ProgramController(_mockProgramRepository.Object, mapper);

    }
    
    [Fact]
    public async Task GetProgram_ProgramDoesNotExist_ReturnsNotFound404()
    {
        // Arrange
        _mockProgramRepository.Setup(r => r.GetProgramAsync("001", "001"))
            .ThrowsAsync(new ItemNotFoundException("Item not found"));
        
        // Act
        var response = await _programController.GetProgram("001","001");
        
        // Asset
        Assert.NotNull(response);
        Assert.IsType<NotFoundResult>(response);
    }
    
    [Fact]
    public async Task GetProgram_ProgramExists_ReturnsOkResult()
    {
        var dbData =  new ProgramData() { Id = "001", ProgramTitle = "Test" };
        _mockProgramRepository.Setup(r => r.GetProgramAsync("001", "001"))
            .ReturnsAsync(dbData);
        
        // Act
        var response = await _programController.GetProgram("001","001");
        
        // Asset
        Assert.NotNull(response);
        Assert.IsType<OkNegotiatedContentResult<ProgramDto>>(response);
    }
}