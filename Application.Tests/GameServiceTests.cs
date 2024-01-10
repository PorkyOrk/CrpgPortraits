using System.Text.Json;
using CrpgP.Application;
using CrpgP.Application.Options;
using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using CrpgP.Domain.Errors;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NSubstitute.ReturnsExtensions;
using Serilog;

namespace Application.UnitTests;

public class GameServiceTests
{
    private IGameRepository _mockRepository = null!;
    private IOptions<MemoryCacheOptions> _mockIOption = null!;

    [SetUp]
    public void Setup()
    {
        _mockRepository = Substitute.For<IGameRepository>();
        _mockIOption = Substitute.For<IOptions<MemoryCacheOptions>>();
        
        // Cache disabled by default
        _mockIOption.Value.Returns(new MemoryCacheOptions { Enabled = false, EntryExpiryInSeconds = 1 });
    }
    
    
    [Test]
    public async Task GetGameByIdAsync_ExistingId_ResultSuccessAndGame()
    {
        // Arrange
        var game = new Game { Name = "Test Game", PortraitSize = new Size() };
        _mockRepository.FindByIdAsync(1).ReturnsForAnyArgs(game);
        var gameService = new GameService(
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.GetGameByIdAsync(1);

        // Assert
        sut.Should().Match<Result>(result => 
            result.IsSuccess == true
            && result.Value!.Equals(game));
    }
    
    [Test]
    public async Task GetGameByIdAsync_ExistingId_ResultNotSuccessAndError()
    {
        // Arrange
        _mockRepository.FindByIdAsync(1).ReturnsNullForAnyArgs();
        var gameService = new GameService(
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.GetGameByIdAsync(1);

        // Assert
        sut.Should().Match<Result>(result => !result.IsSuccess && result.Error == GameErrors.NotFound(1));
    }

    [Test]
    public async Task GetGameByNameAsync_NameExists_ResultSuccessAndGame()
    {
        // Arrange
        var game = new Game { Name = "Test Game", PortraitSize = new Size() };
        _mockRepository.FindByNameAsync("Test Game").ReturnsForAnyArgs(game);
        _mockIOption.Value.Returns(new MemoryCacheOptions { Enabled = false, EntryExpiryInSeconds = 1 });
        var gameService = new GameService(
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.GetGameByNameAsync("Test Game");

        // Assert
        sut.Should().Match<Result>(result => 
            result.IsSuccess == true
            && result.Value!.Equals(game));
    }
    
    [Test]
    public async Task GetGameByNameAsync_NameDoesNotExists_ResultNotSuccessAndError()
    {
        // Arrange
        _mockRepository.FindByNameAsync("Test Game").ReturnsNullForAnyArgs();
        _mockIOption.Value.Returns(new MemoryCacheOptions { Enabled = false, EntryExpiryInSeconds = 1 });
        var gameService = new GameService(
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.GetGameByNameAsync("Test Game");

        // Assert
        sut.Should().Match<Result>(result => !result.IsSuccess && result.Error == GameErrors.NotFoundByName("Test Game"));
    }

    [Test]
    public async Task CreateGameAsync_CreateSuccess_ResultSuccessAndId()
    {
        // Arrange
        var game = new Game { Name = "Test Game", PortraitSize = new Size() };
        _mockRepository.InsertAsync(game).ReturnsForAnyArgs(1);
        
        var gameService = new GameService(
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.CreateGameAsync(game);

        // Assert
        sut.Should().Match<Result>(result => 
            result.IsSuccess == true
            && result.Value!.Equals(1));
    }
    
    [Test]
    public async Task UpdateGameAsync_UpdateSuccess_ResultSuccess()
    {
        // Arrange
        var game = new Game { Name = "Test Game", PortraitSize = new Size() };
        var gameService = new GameService(
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.UpdateGameAsync(game);

        // Assert
        sut.Should().Match<Result>(result => result.IsSuccess == true);
    }
    
    [Test]
    public async Task DeleteGameAsync_DeleteSuccess_ResultSuccess()
    {
        // Arrange
        var gameService = new GameService(
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.DeleteGameAsync(1);

        // Assert
        sut.Should().Match<Result>(result => result.IsSuccess == true);
    }
    
}