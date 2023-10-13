
using InternshipRegistrationAPI.App.Controllers;
using InternshipRegistrationAPI.Core.Dtos;
using InternshipRegistrationAPI.Core.Exceptions;
using InternshipRegistrationAPI.Data.Contracts;
using InternshipRegistrationAPI.Core.Models;
using Moq;
using Xunit;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace InternshipRegistrationAPI.Tests;

public class ProgramControllerTests
{
    private Mock<IProgramRepository> _mockProgramRepository;
    private ProgramController _programController;

    public ProgramControllerTests()
    {
        IMapper mapper = new MapperConfiguration(conf => conf.AddProfile<App.MappingProfile>()).CreateMapper();
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
        var response = await _programController.GetProgram("001", "001");

        // Asset
        Assert.NotNull(response);
        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task GetProgram_ProgramExists_ReturnsOkResult()
    {
        var dbData = new Program { Id = "001", ProgramTitle = "Test" };
        _mockProgramRepository.Setup(r => r.GetProgramAsync("001", "001"))
            .ReturnsAsync(dbData);
        
        var response = await _programController.GetProgram("001", "001");
        
        Assert.NotNull(response);
        var httpResponse = response as OkObjectResult;
        Assert.NotNull(httpResponse);
        var result = httpResponse.Value as ProgramDto;
        Assert.NotNull(result);
        Assert.Equal("001", result.Id);
        Assert.Equal("Test", result.ProgramTitle);
    }

    [Fact] 
    public async Task PostProgram_ValidData_ReturnsOk()
    {
        var postData = new ProgramDto{ Id = "001", ProgramTitle = "Test" };
        var dbResponse = new Program { Id = "001", ProgramTitle = "Test" };

        _mockProgramRepository.Setup(r => r.AddProgramAsync(It.Is<Program>((o) => o.Id == "001" && o.ProgramTitle == "Test")))
            .ReturnsAsync(dbResponse);
        
        var response = await _programController.PostProgram(postData);

        Assert.NotNull(response);
        var httpResponse = response as CreatedAtActionResult;
        Assert.NotNull(httpResponse);
        var result = httpResponse.Value as ProgramDto;
        Assert.NotNull(result);
        Assert.Equal("001", result.Id);
        Assert.Equal("Test", result.ProgramTitle);
    }
}