using CrpgP.Application;
using CrpgP.Application.Cache;
using CrpgP.Domain;
using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using Serilog;

namespace Application.UnitTests;

public class PortraitServiceTests
{
    private IPortraitRepository _mockRepository = null!;

    [SetUp]
    public void Setup()
    {
        _mockRepository = Substitute.For<IPortraitRepository>();
    }

    [Test]
    public async Task GetPortraitByIdAsync_ExistingId_ResultSuccessAndPortrait()
    {
        // Arrange
        var portrait = new Portrait { FileName = "portrait", Size = new Size()};
        _mockRepository.FindByIdAsync(1).ReturnsForAnyArgs(portrait);
        var portraitService = new PortraitService(
            _mockRepository,
            Substitute.For<ICacheService>(),
            Substitute.For<ILogger>());

        // Act
        var sut = await portraitService.GetPortraitByIdAsync(1);

        // Assert
        sut.Should().Match<Result>(result => 
            result.IsSuccess == true
            && result.Value!.Equals(portrait));
    }

    [Test]
    public async Task GetPortraitsByIdsAsync_ExistingIds_ResultSuccessAndPortraits()
    {
        // Arrange
        var portraits = new Dictionary<int, Portrait?>
        {
            { 1, new Portrait { FileName = "portrait-1", Size = new Size() } },
            { 2, new Portrait { FileName = "portrait-2", Size = new Size() } },
            { 3, null }
        };
        _mockRepository.FindByIdsAsync([1,2,3]).ReturnsForAnyArgs(portraits);
        var portraitService = new PortraitService(
            _mockRepository,
            Substitute.For<ICacheService>(),
            Substitute.For<ILogger>());

        // Act
        var sut = await portraitService.GetPortraitsByIdsAsync([1,2,3]);
        
        // Assert
        sut.Should().Match<Result>(result => 
            result.IsSuccess == true
            && result.Value!.Equals(portraits));
    }

    [Test]
    public async Task CreatePortraitAsync_CreateSuccess_ResultSuccessAndId()
    {
        // Arrange
        var portrait = new Portrait { FileName = "portrait", Size = new Size()};
        _mockRepository.InsertAsync(portrait).ReturnsForAnyArgs(1);
        var portraitService = new PortraitService(
            _mockRepository,
            Substitute.For<ICacheService>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await portraitService.CreatePortraitAsync(portrait);

        // Assert
        sut.Should().Match<Result>(result => 
            result.IsSuccess == true
            && result.Value!.Equals(1));
    }
    
    [Test]
    public async Task UpdatePortraitAsync_UpdateSuccess_ResultSuccess()
    {
        // Arrange
        var portrait = new Portrait { FileName = "portrait", Size = new Size()};
        var portraitService = new PortraitService(
            _mockRepository,
            Substitute.For<ICacheService>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await portraitService.UpdatePortraitAsync(portrait);

        // Assert
        sut.Should().Match<Result>(result => result.IsSuccess == true);
    }
    
    [Test]
    public async Task DeletePortraitAsync_DeleteSuccess_ResultSuccess()
    {
        // Arrange
        var gameService = new PortraitService(
            _mockRepository,
            Substitute.For<ICacheService>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.DeletePortraitAsync(1);

        // Assert
        sut.Should().Match<Result>(result => result.IsSuccess == true);
    }

}