using System.Text.Json;
using CrpgP.Application;
using CrpgP.Application.Options;
using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Serilog;

namespace Application.UnitTests;

public class PortraitServiceTests
{
    private IPortraitRepository _mockRepository = null!;
    private IOptions<MemoryCacheOptions> _mockIOption = null!;

    [SetUp]
    public void Setup()
    {
        _mockRepository = Substitute.For<IPortraitRepository>();
        _mockIOption = Substitute.For<IOptions<MemoryCacheOptions>>();
        
        // Cache disabled by default
        _mockIOption.Value.Returns(new MemoryCacheOptions { Enabled = false, EntryExpiryInSeconds = 1 });
    }

    [Test]
    public async Task GetPortraitByIdAsync_ExistingId_ResultSuccessAndPortrait()
    {
        // Arrange
        var portrait = new Portrait { FileName = "portrait", Size = new Size()};
        _mockRepository.FindByIdAsync(1).ReturnsForAnyArgs(portrait);
        var portraitService = new PortraitService(
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
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
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
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
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
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
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
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
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.DeletePortraitAsync(1);

        // Assert
        sut.Should().Match<Result>(result => result.IsSuccess == true);
    }

}