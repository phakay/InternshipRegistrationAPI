using InternshipRegistrationAPI.Data.Contracts;
using InternshipRegistrationAPI.Data.DataModels;
using InternshipRegistrationAPI.Data.Repositories;
using Microsoft.Azure.Cosmos;
using Moq;
using System.Net;
using Xunit;

namespace InternshipRegistrationAPI.Tests;

public class ProgramRepositoryTests
{
    private ProgramRepository _repo;
    private Mock<Container> _mockContainer;
    
    public ProgramRepositoryTests()
    {
        string containerId = "Programs";
        string partitionKeyPath = "/id";

        var _mockDbContext = new Mock<IApplicationDbContext>();
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

        _mockDbContext.Setup(c => c.Database)
            .Returns(mockDb.Object);

        _repo = new ProgramRepository(_mockDbContext.Object);
    }

    [Fact]
    public async Task GetProgram_ProgramExists_Success()
    {
        // Arrange
        var dbData = new ProgramData() { Id = "001", ProgramTitle = "Test" };
        var _mockItemResponse = new Mock<ItemResponse<ProgramData>>();
        _mockItemResponse.SetupGet(x => x.Resource).Returns(dbData);

        _mockContainer.Setup(x => x.ReadItemAsync<ProgramData>(
            "001", new PartitionKey("001"),
            It.IsAny<ItemRequestOptions>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(_mockItemResponse.Object);

        _mockContainer
            .Setup(x => x.ReadItemAsync<ProgramData>(
                It.IsAny<string>(), 
                It.IsAny<PartitionKey>(),
                It.IsAny<ItemRequestOptions>(),
                It.IsAny<CancellationToken>()
               )).Callback(AssertIdAndPartitionKey)
            .ReturnsAsync(_mockItemResponse.Object);


        // Act
        var response = await _repo.GetProgramAsync("001", "001");

        // Assert
        Assert.NotNull(response);
        Assert.Equal("001", response.Id);
        Assert.Equal("001", response.PartitionKey);
        Assert.Equal("Test", response.ProgramTitle);
    }


    [Fact]
    public async Task Add_ValidData_Success()
    {
        // Arrange
        var data = new ProgramData() { Id = "001", ProgramTitle = "Test" };
        var _mockItemResponse = new Mock<ItemResponse<ProgramData>>();
        _mockItemResponse.SetupGet(x => x.Resource).Returns(data);

        _mockContainer.Setup(x => x.CreateItemAsync<ProgramData>(
            data,
            new PartitionKey(data.PartitionKey),
            It.IsAny<ItemRequestOptions>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(_mockItemResponse.Object);

        _mockContainer
            .Setup(x => x.ReadItemAsync<ProgramData>(
                data.Id,
                new PartitionKey(data.PartitionKey),
                It.IsAny<ItemRequestOptions>(),
                It.IsAny<CancellationToken>()
               )).ThrowsAsync(new CosmosException("Not found", HttpStatusCode.NotFound, (int)HttpStatusCode.NotFound, default, default));

        // Act
        var response = await _repo.AddProgramAsync(data);

        // Assert
        Assert.NotNull(response);
        Assert.Equal("001", response.Id);
        Assert.Equal("001", response.PartitionKey);
        Assert.Equal("Test", response.ProgramTitle);
    }


    private void AssertIdAndPartitionKey(string id, PartitionKey pk, ItemRequestOptions ops, CancellationToken token)
    {
        Assert.Equal("001", id);
        Assert.Equal(new PartitionKey("001"), pk);
    }
}
