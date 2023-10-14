using InternshipRegistrationAPI.Data.Contracts;
using InternshipRegistrationAPI.Data.Repositories;
using Microsoft.Azure.Cosmos;
using Moq;
using System.Net;
using InternshipRegistrationAPI.Core.Exceptions;
using InternshipRegistrationAPI.Core.Models;
using Xunit;

namespace InternshipRegistrationAPI.Tests;

public class ProgramRepositoryTests
{
    private ProgramRepository _repo;
    private Mock<Container> _mockContainer;

    public ProgramRepositoryTests()
    {
        string containerId = "Programs";
        string partitionKeyPath = "/Type";

        var mockDbContext = new Mock<IApplicationDbContext>();
        var mockContainerResponse = new Mock<ContainerResponse>();

        _mockContainer = new Mock<Container>();

        mockContainerResponse.Setup(c => c.Container).Returns(_mockContainer.Object);

        var mockDb = new Mock<Database>();
        mockDb.Setup(m => m.CreateContainerIfNotExistsAsync(
                containerId,
                partitionKeyPath,
                It.IsAny<int?>(),
                It.IsAny<RequestOptions>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(mockContainerResponse.Object));

        mockDbContext.Setup(c => c.Database)
            .Returns(mockDb.Object);

        _repo = new ProgramRepository(mockDbContext.Object);
    }

    [Fact]
    public async Task GetProgramAsync_ProgramExists_Success()
    {
        // Arrange
        var dbData = new Program{ Id = "001", ProgramTitle = "Test", Type = "program"};
        var mockItemResponse = new Mock<ItemResponse<Program>>();
        mockItemResponse.SetupGet(x => x.Resource).Returns(dbData);

        _mockContainer
            .Setup(x => x.ReadItemAsync<Program>(
                It.IsAny<string>(),
                It.IsAny<PartitionKey>(),
                It.IsAny<ItemRequestOptions>(),
                It.IsAny<CancellationToken>()
            )).Callback(AssertIdAndPartitionKey)
            .ReturnsAsync(mockItemResponse.Object);

        // Act
        var response = await _repo.GetDocumentAsync("001", "program");

        // Assert
        Assert.NotNull(response);
        Assert.Equal("001", response.Id);
        Assert.Equal("program", response.PartitionKey);
        Assert.Equal("Test", response.ProgramTitle);
    }

    [Fact]
    public async Task GetProgramAsync_ProgramDoesNotExist_ThrowsItemNotFoundException()
    {
        // Arrange
        _mockContainer.Setup(x => x.ReadItemAsync<Program>(
                "001", new PartitionKey("program"),
                It.IsAny<ItemRequestOptions>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CosmosException("Not found", HttpStatusCode.NotFound, (int)HttpStatusCode.NotFound,
                default, default));

        // Act
        var response = await Record.ExceptionAsync(() => _repo.GetDocumentAsync("001", "program"));


        // Assert
        Assert.NotNull(response);
        Assert.IsType<ItemNotFoundException>(response);
    }

    [Fact]
    public async Task AddProgramAsync_ValidData_Success()
    {
        // Arrange
        var data = new Program { Id = "001", ProgramTitle = "Test", Type = "program"};
        var mockItemResponse = new Mock<ItemResponse<Program>>();
        mockItemResponse.SetupGet(x => x.Resource).Returns(data);

        _mockContainer.Setup(x => x.CreateItemAsync<Program>(
                data,
                new PartitionKey(data.PartitionKey),
                It.IsAny<ItemRequestOptions>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockItemResponse.Object);

        _mockContainer
            .Setup(x => x.ReadItemAsync<Program>(
                data.Id,
                new PartitionKey(data.PartitionKey),
                It.IsAny<ItemRequestOptions>(),
                It.IsAny<CancellationToken>()
            )).ThrowsAsync(new CosmosException("Not found", HttpStatusCode.NotFound, (int)HttpStatusCode.NotFound,
                default, default));

        // Act
        var response = await _repo.AddDocumentAsync(data);

        // Assert
        Assert.NotNull(response);
        Assert.Equal("001", response.Id);
        Assert.Equal("program", response.PartitionKey);
        Assert.Equal("Test", response.ProgramTitle);
    }


    private void AssertIdAndPartitionKey(string id, PartitionKey pk, ItemRequestOptions ops, CancellationToken token)
    {
        Assert.Equal("001", id);
        Assert.Equal(new PartitionKey("program"), pk);
    }
}